import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoardComponent, GameSidenavComponent } from '../game.imports';
import { GameHubService, GameService } from '../services/game-services.imports';
import { Game } from '../game.models';
import { Observable } from 'rxjs';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit, OnDestroy {

    game: Game;
    private paramsSub: any;

    @ViewChild(GameBoardComponent)
    private board: GameBoardComponent;
    @ViewChild(GameSidenavComponent)
    private sideNav: GameSidenavComponent;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService,
        private gameService: GameService,
        private snackbar: SnackbarService) { }

    ngOnInit() {
        this.paramsSub = this.route.params
            .subscribe(params => {
                var gameId = +params["id"];
                this.connectToGame(gameId);
            });
    }

    private connectToGame(gameId: number) {
        this.gameHub.connect()
            //Uncomment line below to debug sizing of fields. Comment one above!
            //Observable.of(true).delay(0) 
            .mergeMap(connected => {
                if (connected) {
                    return this.gameHub
                        .subscribeToGame(gameId)
                }
                Observable.throw("Could not connect to game hub");
            })
            .mergeMap(connectionId => {
                return this.gameService
                    .getGame(gameId);
            })
            .subscribe(result => {
                this.game = result;

                this.board.initialize(this.game);
                this.sideNav.initialize(this.game);
            }, err => {
                this.snackbar.openSnackbar(err);
            });
    }

    ngOnDestroy() {
        if (this.paramsSub)
            this.paramsSub.unsubscribe();
        if (this.gameHub)
            this.gameHub.disconnect();
    }
}
