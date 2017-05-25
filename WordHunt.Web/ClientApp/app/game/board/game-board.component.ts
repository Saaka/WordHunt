import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameHubService } from '../services/game-services.imports';
import { Game, Field } from '../game.models';

@Component({
    selector: 'game-board',
    templateUrl: './game-board.component.html',
    styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent {

    game: Game;
    rows: Field[][] = [];
    rowFlex: number;
    colFlex: number;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) {
    }

    initialize(game: Game) {
        this.game = game;
        this.createRows();
        this.calculateFlex();
    }

    private calculateFlex() {
        this.colFlex = 100 / this.game.boardHeight;
        this.rowFlex = 100 / this.game.boardWidth;
    }

    private createRows() {
        var tempArray = this.game.fields;
        var index = 0;
        while (tempArray.length) {
            this.rows[index] = tempArray.splice(0, this.game.boardWidth);
            index++;
        }
    }
}
