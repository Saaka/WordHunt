import { Component, Input, OnInit, OnDestroy, ViewChildren, QueryList } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameHubService } from '../services/game-services.imports';
import { Game, Field, FieldChecked } from '../game.models';
import { MapFieldComponent } from './map-board-components';

@Component({
    selector: 'map-board',
    templateUrl: './map-board.component.html',
    styleUrls: ['./map-board.component.scss']
})
export class MapBoardComponent {

    game: Game;
    rows: Field[][] = [];
    rowFlex: number;
    colFlex: number;

    @ViewChildren('mapField')
    mapFields: QueryList<MapFieldComponent>;

    constructor(private route: ActivatedRoute,
        private gameHub: GameHubService) {
    }

    initialize(game: Game) {
        this.game = game;
        this.createRows();
        this.calculateFlex();

        this.gameHub.fieldChecked(this.onFieldChecked);
    }

    onFieldChecked = (args: FieldChecked) => {

        var component = this.mapFields.filter(f => f.field.id == args.fieldId)[0];
        var team = this.game.teams.filter(t => t.id == args.teamId)[0];
        if (team)
            component.onFieldChecked(args, team.color);
        else
            component.onFieldChecked(args);
    }

    private calculateFlex() {
        const baseFlexValue: number = 95;

        this.colFlex = baseFlexValue / this.game.boardHeight;
        this.rowFlex = baseFlexValue / this.game.boardWidth;
    }

    private createRows() {
        var tempArray = this.game.fields;
        var index = 0;
        while (tempArray.length) {
            this.rows[index] = tempArray.splice(0, this.game.boardWidth);
            index++;
        }
    }
}
