import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameHubService } from '../services/game-services.imports';
import { Game, Field } from '../game.models';

@Component({
    selector: 'game-board',
    templateUrl: './game-board.component.html',
    styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent{

    game: Game;
    rows: Field[][] = [];

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) {
    }

    initialize(game: Game) {
        this.game = game;
        this.createRows(this.game);
    }

    private createRows(game: Game) {
        var tempArray = game.fields;
        var index = 0;
        while (tempArray.length) {
            this.rows[index] = tempArray.splice(0, game.boardWidth);
            index++;
        }
    }
}
