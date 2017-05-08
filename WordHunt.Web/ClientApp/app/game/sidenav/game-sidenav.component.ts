import { Component } from '@angular/core';

import { GameHubService } from '../services/game-services.imports';

@Component({
    selector: 'game-sidenav',
    templateUrl: './game-sidenav.component.html',
    styleUrls: ['./game-sidenav.component.scss']
})
export class GameSidenavComponent {

    constructor(private gameHub: GameHubService) { }

    initialize() {

        this.gameHub.messageReceived(this.messageReceived);
    }

    private messageReceived(message: string) {

        console.log('** SIDENAV RECEIVED ** ' + message);
    }

    sendMessage() {
        this.gameHub.sendMessage('** SIDENAV CLICKED **');
    }
}
