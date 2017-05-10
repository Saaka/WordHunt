import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoardComponent, GameSidenavComponent } from '../game.imports';
import { GameHubService, GameService } from '../services/game-services.imports';
import { Game } from '../game.models';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit, OnDestroy {

    gameId: number;
    game: Game;
    private paramsSub: any;

    @ViewChild(GameBoardComponent)
    private board: GameBoardComponent;
    @ViewChild(GameSidenavComponent)
    private sideNav: GameSidenavComponent;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService,
        private gameService: GameService) { }

    ngOnInit() {
        this.paramsSub = this.route.params
            .subscribe(params => {
                this.gameId = +params['id'];
                this.gameService
                    .getGame(this.gameId)
                    .mergeMap(response => {
                        this.game = response;
                        return this.gameHub
                            .connect();
                    })
                    .subscribe(() => {
                        this.board.initialize();
                        this.sideNav.initialize();
                    });
            });
    }

    ngOnDestroy() {
        if (this.paramsSub)
            this.paramsSub.unsubscribe();
        if (this.gameHub)
            this.gameHub.disconnect();
    }
}
