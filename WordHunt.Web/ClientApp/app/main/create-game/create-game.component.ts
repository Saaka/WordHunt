import { Component, OnInit } from '@angular/core';

import { GameCreate, GameTeamCreate } from './game.models';
import { CreateGameService } from './service/create-game.service';
import { UserService } from '../../core/user.service';
import { GameNavigation } from '../../core/navigation/game-navigation.service';

@Component({
    selector: 'create-game',
    templateUrl: './create-game.component.html',
    styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent {

    private game: GameCreate;
    private loading: boolean = false;

    constructor(private userService: UserService,
        private createGameService: CreateGameService,
        private gameNavService: GameNavigation) {
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
        this.game.languageId = 1;

        this.createDefaultGameTeams(this.game);
    }

    private createDefaultGameTeams(game: GameCreate) {

        game.teams = [];
        for (let i = 0; i < game.teamCount; i++) {
            game.teams.push({
                fieldCount: 9,
                name: 'Team' + (i+1)
            });
        }
    }

    createGame() {
        this.loading = true;
        this.createGameService
            .createGame(this.game)
            .subscribe(res => {
                this.gameNavService.goToGame(res.gameId);
            }, err => {
                console.log(err);
                this.loading = false;
            });
    }

}
