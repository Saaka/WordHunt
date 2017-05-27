import { Component, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { Field } from '../game.models';

@Component({
    selector: 'game-field',
    templateUrl: './game-field.component.html',
    styleUrls: ['./game-field.component.scss']
})
export class GameFieldComponent implements OnChanges {

    @Input() field: Field;
    fieldClass: string;
    colorClass: string = "";

    constructor() { }

    private clickWord() {
        console.log(this.field.word);
        this.colorClass = "red";
    }

    ngOnChanges(changes: SimpleChanges) {
        for (let propName in changes) {
            if (propName === 'field') {
                this.fieldChanged(changes[propName]);
            }
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
        }
    }
}
