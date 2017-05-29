import { Component, Input } from '@angular/core';

import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, Team, TeamChanged } from '../game.models';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'game-navigation',
    templateUrl: './game-navigation.component.html',
    styleUrls: ['./game-navigation.component.scss']
})
export class GameNavigationComponent {

    game: Game;
    teamName: string = "Current Team";
    colorClass: string = "team-not-selected";
    skippingTurn: boolean = false;

    @Input()
    isMap: boolean = false;

    constructor(private gameHub: GameHubService,
        private gameService: GameService,
        private snackbar: SnackbarService) { }

    private skipRound() {
        this.isSkippingTurn(true);
        this.gameService
            .skipRound(this.game.id)
            .subscribe(() => this.isSkippingTurn(false),
            (e) => {
                this.isSkippingTurn(false);
                this.snackbar.openSnackbar(e.error);
            });
    }

    private isSkippingTurn(isSkipping: boolean) {
        this.skippingTurn = isSkipping;
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
