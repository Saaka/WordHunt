import { Injectable } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';
import { GameEndedDialog, GameEndedDialogData, GameEndedDialogResult } from '../dialogs/dialog.imports';

@Injectable()
export class GameDialogsService {

    constructor(private dialog: MdDialog) { }

    openEndGameDialog(data: GameEndedDialogData) {
        let dialogRef = this.dialog.open(GameEndedDialog, { data: data });
        return dialogRef.afterClosed().map(result => <GameEndedDialogResult>result);
    }
}