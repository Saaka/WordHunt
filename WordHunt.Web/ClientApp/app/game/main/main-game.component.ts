import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoardComponent, GameSidenavComponent } from '../game.imports';
import { GameHubService, GameService } from '../services/game-services.imports';
import { Game } from '../game.models';
import { Observable } from 'rxjs';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit, OnDestroy {
    
    game: Game;
    private paramsSub: any;

    //@ViewChild(GameBoardComponent)
    //private board: GameBoardComponent;
    //@ViewChild(GameSidenavComponent)
    //private sideNav: GameSidenavComponent;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService,
        private gameService: GameService) { }

    ngOnInit() {
        this.paramsSub = this.route.params
            .subscribe(params => {
                var gameId = +params["id"];
                //this.connectToGame(gameId);
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
            .mergeMap(result => {
                this.game = result;

                //Make sure variables are set in the components
                return Observable.of(true).delay(50);
            })
            .subscribe(() => {
                //this.board.initialize();
                //this.sideNav.initialize();
            });
    }

    private handleMessage(message: string) {
        console.log(message);
    }

    ngOnDestroy() {
        if (this.paramsSub)
            this.paramsSub.unsubscribe();
        if (this.gameHub)
            this.gameHub.disconnect();
    }
}
