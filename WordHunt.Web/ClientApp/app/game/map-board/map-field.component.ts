import { Component, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';

import { Game, Field, FieldChecked } from '../game.models';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'map-field',
    templateUrl: './map-field.component.html',
    styleUrls: ['./map-field.component.scss']
})
export class MapFieldComponent implements OnChanges {

    @Input() field: Field;
    @Input() game: Game;
    fieldClass: string = "";
    colorClass: string = "";
    isChecking: boolean = false;

    constructor(private snackbar: SnackbarService) { }

    ngOnChanges(changes: SimpleChanges) {
        for (let propName in changes) {
            if (propName === 'field') {
                this.fieldChanged(changes[propName]);
            }
        }
    }

    onFieldChecked(args: FieldChecked, color?: string) {
        this.updateField(args, color);
    }

    updateField(updated: FieldChecked, color?: string) {
        this.field.checked = updated.checked;
    }

    //Set color
    setColor(field: Field) {
        if (field.type == 0)
            this.colorClass = "empty-field";
        else if (field.type == 1)
            this.colorClass = this.field.color + "-team";
        else if (field.type == 2)
            this.colorClass = "trap-field";
    }

    //Set font size when Field is set.
    private fieldChanged(field: SimpleChange) {
        if (field.firstChange) {
            var current = <Field>field.currentValue;
            if (current.word.length > 10)
                this.fieldClass = 'long-field';
            else
                this.fieldClass = 'short-field';

            this.setColor(current);
        }
    }
}
