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

    @Input() game: Game;

    rows: Field[][] = [];

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) {
    }

    initialize() {

        this.createRows();
        //this.gameHub.messageReceived(this.messageReceived);
    }

    private createRows() {
        var tempArray = this.game.fields;
        var index = 0;
        while (tempArray.length) {
            this.rows[index] = tempArray.splice(0, this.game.boardWidth);
            index++;
        }
    }

    private messageReceived(message: string) {
        console.log('** BOARD RECEIVED ** ' + message);
    }
}
