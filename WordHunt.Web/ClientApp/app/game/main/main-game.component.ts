import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GameBoardComponent, GameSidenavComponent, GameNavigationComponent } from '../game.imports';
import { GameHubService, GameService, GameDialogsService } from '../services/game-services.imports';
import { Game, GameEnded, GameRestarted } from '../game.models';
import { Observable } from 'rxjs';
import { SnackbarService } from '../../core/core.imports';
import { GameNavigation } from '../../core/navigation/game-navigation.service';
import { GameEndedDialogResult } from '../dialogs/dialog.imports';

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
    @ViewChild(GameNavigationComponent)
    private gameNavigation: GameNavigationComponent;

    constructor(private route: ActivatedRoute,
        private router: Router, 
        private gameHub: GameHubService,
        private gameService: GameService,
        private snackbar: SnackbarService,
        private dialogService: GameDialogsService,
        private gameNavigationService: GameNavigation) { }

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
        this.gameNavigationService.goToGame(args.gameId);
    }

    onGameEnded = (args: GameEnded) => {
        var team = this.getTeam(args.winningTeamId);

        this.dialogService.openEndGameDialog({
            canRestart: this.gameService.canRestart(this.game),
            teamName: team.name,
            teamColor: team.color 
        }).subscribe(result => {
            if (result == GameEndedDialogResult.mainMenu)
                this.router.navigate(['/main']);
            else if (result == GameEndedDialogResult.newGame)
                this.createNewGame();
        });
    }

    private createNewGame() {


        this.gameService
            .restartGame(this.game.id)
            .subscribe(res => {
                this.snackbar.openSnackbar("Restarting game");
            }, err => {
                this.snackbar.openSnackbar(err);
            });  
    }

    private getTeam(teamId: number) {
        return this.game.teams.filter(t => t.id == teamId)[0];
    }

    ngOnDestroy() {
        if (this.paramsSub)
            this.paramsSub.unsubscribe();
        if (this.gameHub)
            this.gameHub.disconnect();
    }
}
