import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MapBoardComponent, GameSidenavComponent, GameNavigationComponent } from '../game.imports';
import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, GameEnded, GameRestarted } from '../game.models';
import { Observable } from 'rxjs';
import { GameNavigation } from '../../core/navigation/game-navigation.service';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'game-map',
    templateUrl: './game-map.component.html',
    styleUrls: ['./game-map.component.scss']
})
export class GameMapComponent implements OnInit, OnDestroy {

    game: Game;
    private paramsSub: any;

    @ViewChild(MapBoardComponent)
    private board: MapBoardComponent;
    @ViewChild(GameSidenavComponent)
    private sideNav: GameSidenavComponent;
    @ViewChild(GameNavigationComponent)
    private gameNavigation: GameNavigationComponent;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService,
        private gameService: GameService,
        private snackbar: SnackbarService,
        private gameNavigationService: GameNavigation) { }

    ngOnInit() {
        this.paramsSub = this.route.parent.params
            .subscribe(params => {
                var gameId = +params["id"];
                this.connectToGame(gameId);
            });
    }

    private connectToGame(gameId: number) {
        this.gameHub.connect()
            .mergeMap(connected => {
                if (connected) {
                    return this.gameHub
                        .subscribeToGame(gameId)
                }
                Observable.throw("Could not connect to game hub");
            })
            .mergeMap(connectionId => {
                return this.gameService
                    .getGameMap(gameId);
            })
            .subscribe(result => {
                this.game = result;

                this.board.initialize(this.game);
                this.sideNav.initialize(this.game);
                this.gameNavigation.initialize(this.game);


                this.init();
            }, err => {
                this.snackbar.openSnackbar(err);
            });
    }

    private init() {
        this.gameHub.gameEnded(this.onGameEnded);
        this.gameHub.gameRestarted(this.onGameRestarted);
    }

    onGameRestarted = (args: GameRestarted) => {
        
        this.gameHub.disconnect();
        this.gameNavigationService.goToGameMap(args.gameId);
    }

    onGameEnded = (args: GameEnded) => {
        console.log(args.winningTeamId);
    }

    ngOnDestroy() {
        if (this.paramsSub)
            this.paramsSub.unsubscribe();
        if (this.gameHub)
            this.gameHub.disconnect();
    }
}
