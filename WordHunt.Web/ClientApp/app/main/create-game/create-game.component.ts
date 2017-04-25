import { Component, OnInit } from '@angular/core';

import { GameCreate } from './game.models';
import { CreateGameService } from './service/create-game.service';
import { UserService } from '../../core/user.service';

@Component({
    selector: 'create-game',
    templateUrl: './create-game.component.html',
    styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent {

    private game: GameCreate;

    constructor(private userService: UserService,
        private createGameService: CreateGameService) {
        this.createDefaultGame();
    }

    private createDefaultGame() {
        this.game = new GameCreate();
        this.game.name = 'New game';
        this.game.boardHeight = this.game.boardWidth = 5;
        this.game.trapCount = 1;
        this.game.teamCount = 2;
        this.game.type = 'SingleDevice';
        this.game.endMode = 'EndTurn';
        this.game.userId = this.userService.userId();
    }

    createGame() {
        console.log(this.game);
        this.createGameService.createGame(this.game);
    }

}
