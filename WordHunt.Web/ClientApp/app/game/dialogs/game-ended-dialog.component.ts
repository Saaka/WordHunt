import { Component, Inject } from '@angular/core';
import { MD_DIALOG_DATA } from '@angular/material';
import { MdDialogRef } from '@angular/material';

import { GameEndedDialogData, GameEndedDialogResult } from './game-ended-dialog.model';

@Component({
    selector: 'game-ended-dialog',
    templateUrl: './game-ended-dialog.component.html',
})
export class GameEndedDialog {

    //result: GameEndedDialogResult = GameEndedDialogResult.doNothing;

    constructor( @Inject(MD_DIALOG_DATA) public data: GameEndedDialogData
        //,        public dialogRef: MdDialogRef<GameEndedDialogResult>
    ) { }

    public restartGame() {
        //this.result = GameEndedDialogResult.newGame;
    }

    public goToMainMenu() {
        //this.result = GameEndedDialogResult.mainMenu;
    }
}