import { Component, OnInit } from '@angular/core';

import { GameCreate } from './game.models';
import { UserService } from '../../core/user.service';

@Component({
    selector: 'create-game',
    templateUrl: './create-game.component.html',
    styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent {

    private game: GameCreate;

    constructor(private userService: UserService) {
        this.createDefaultGame();
    }

    private createDefaultGame() {
        this.game = new GameCreate();
        this.game.name = 'New game';
        this.game.boardHeight = this.game.boardWidth = 5;
        this.game.trapCount = 1;
        this.game.teamCount = 2;
        this.game.type = 'SingleDevice';
        this.game.userId = this.userService.userId();
    }

    createGame() {
        console.log(this.game);
    }

}
