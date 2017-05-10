import { Component, Input } from '@angular/core';
import { Field } from '../game.models';

@Component({
    selector: 'game-field',
    templateUrl: './game-field.component.html',
    styleUrls: ['./game-field.component.scss']
})
export class GameFieldComponent {

    @Input() field: Field;

    constructor() { }
}
