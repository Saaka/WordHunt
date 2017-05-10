import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameHubService } from '../services/game-services.imports';
import { Game } from '../game.models';

@Component({
    selector: 'game-board',
    templateUrl: './game-board.component.html',
    styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent{

    @Input() game: Game;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) {
    }

    initialize() {
        var connected = 'Client connected to game: ' + this.game.name + ' Time: ' + new Date().toString();

        this.gameHub.messageReceived(this.messageReceived);
        this.gameHub.sendMessage(connected);
    }

    invoke() {
        this.gameHub.sendMessage('** BOARD Client Clicked **');
    }

    private messageReceived(message: string) {
        console.log('** BOARD RECEIVED ** ' + message);
    }
}
