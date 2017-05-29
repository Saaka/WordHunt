import { Component, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';

import { GameService } from '../services/game-services.imports';
import { Game, Field, FieldChecked } from '../game.models';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'game-field',
    templateUrl: './game-field.component.html',
    styleUrls: ['./game-field.component.scss']
})
export class GameFieldComponent implements OnChanges {

    @Input() field: Field;
    @Input() game: Game;
    fieldClass: string = "";
    colorClass: string = "";
    disableClick: boolean = false;
    isChecking: boolean = false;

    constructor(private gameService: GameService,
        private snackbar: SnackbarService) { }

    private clickWord() {
        if (!this.disableClick) {
            this.isChecking = true;
            this.gameService
                .checkField(this.game.id, this.field.id)
                .subscribe(() => this.isChecking = false,
                (e) => {
                    this.isChecking = false;
                    this.snackbar.openSnackbar(e.error);
                });
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        for (let propName in changes) {
            if (propName === 'field') {
                this.fieldChanged(changes[propName]);
            }
        }
    }

    onFieldChecked(args: FieldChecked, color?: string) {
        this.disableClick = true;
        this.updateField(args, color);
        this.setColor(this.field);
    }

    updateField(updated: FieldChecked, color?: string) {
        this.field.checked = updated.checked;
        this.field.teamId = updated.teamId;
        this.field.type = updated.type;
        this.field.color = color;
    }

    //Set color
    setColor(field: Field) {
        if (field.checked) {
            if (field.type == 0)
                this.colorClass = "empty-field";
            else if (field.type == 1)
                this.colorClass = this.field.color + "-team";
            else if (field.type == 2)
                this.colorClass = "trap-field";
        }
    }

    //Set font size when Field is set.
    private fieldChanged(field: SimpleChange) {
        if (field.firstChange) {
            var current = <Field>field.currentValue;
            if (current.word.length > 10)
                this.fieldClass = 'long-field';
            else
                this.fieldClass = 'short-field';

            this.disableClick = current.checked;
            this.setColor(current);
        }
    }
}
