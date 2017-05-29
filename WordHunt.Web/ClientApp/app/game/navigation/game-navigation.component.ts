import { Component, Input } from '@angular/core';

import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, Team, TeamChanged } from '../game.models';

@Component({
    selector: 'game-navigation',
    templateUrl: './game-navigation.component.html',
    styleUrls: ['./game-navigation.component.scss']
})
export class GameNavigationComponent {

    game: Game;
    teamName: string = "Current Team";
    colorClass: string = "team-not-selected";

    constructor(private gameHub: GameHubService,
        private gameService: GameService) { }

    private skipRound() {
        this.gameService
            .skipRound(this.game.id)
            .subscribe();
    }

    public setCurrentTeam(teamId: number) {
        let team = this.game.teams.filter(t => t.id == teamId)[0];
        this.teamName = team.name;
        this.colorClass = team.color + "-team";
    }

    onTeamChanged = (event: TeamChanged) => {
        this.setCurrentTeam(event.newTeamId);
    }

    initialize(game: Game) {
        this.game = game;
        this.setCurrentTeam(this.game.currentTeamId);
        this.gameHub.teamChanged(this.onTeamChanged);
    }
}
