import { Component, Input } from '@angular/core';

import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, TeamChanged } from '../game.models';

@Component({
    selector: 'game-navigation',
    templateUrl: './game-navigation.component.html',
    styleUrls: ['./game-navigation.component.scss']
})
export class GameNavigationComponent {

    @Input() game: Game;

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

    initialize() {
        this.gameHub.teamChanged(this.onTeamChanged);
    }
}
