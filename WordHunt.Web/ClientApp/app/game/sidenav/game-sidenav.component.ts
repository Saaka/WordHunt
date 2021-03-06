﻿import { Component, Input } from '@angular/core';

import { GameHubService, GameService } from '../services/game-services.imports';
import { Game, TeamChanged } from '../game.models';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'game-sidenav',
    templateUrl: './game-sidenav.component.html',
    styleUrls: ['./game-sidenav.component.scss']
})
export class GameSidenavComponent {

    game: Game;
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

    private isSkippingTurn(value: boolean) {
        this.skippingTurn = value;
    }

    onTeamChanged = (event: TeamChanged) => {
        console.log(`Team changed. New team: ${event.newTeamId}. Previous team: ${event.lastTeamId}.`);
    }

    initialize(game: Game) {
        this.game = game;
        this.gameHub.teamChanged(this.onTeamChanged);
    }
}
