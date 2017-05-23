import { Component, Input } from '@angular/core';

import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, TeamChanged } from '../game.models';

@Component({
    selector: 'game-sidenav',
    templateUrl: './game-sidenav.component.html',
    styleUrls: ['./game-sidenav.component.scss']
})
export class GameSidenavComponent {

    game: Game;

    constructor(private gameHub: GameHubService,
        private gameService: GameService) { }

    private skipRound() {
        this.gameService
            .skipRound(this.game.id)
            .subscribe();
    }

    onTeamChanged(event: TeamChanged) {
        console.log(`Team changed. New team: ${event.newTeamId}. Previous team: ${event.lastTeamId}.`);
    }

    initialize(game: Game) {
        this.game = game;
        this.gameHub.teamChanged(this.onTeamChanged);
    }
}
