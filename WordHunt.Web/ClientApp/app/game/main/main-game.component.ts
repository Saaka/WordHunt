import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameBoardComponent, GameSidenavComponent } from '../game.imports';
import { GameHubService } from '../services/game-services.imports';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit, OnDestroy {

    gameId: number;
    private paramsSub: any;

    @ViewChild(GameBoardComponent)
    private board: GameBoardComponent;
    @ViewChild(GameSidenavComponent)
    private sideNav: GameSidenavComponent;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) { }

    ngOnInit() {

        this.paramsSub = this.route.params
            .subscribe(params => {
                this.gameId = +params['id'];
                this.gameHub.connect()
                    .subscribe((connected) => {
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
