import { Component, Inject } from "@angular/core";
import { MD_DIALOG_DATA } from "@angular/material";
import { MdDialogRef } from "@angular/material";

import { GameEndedDialogData, GameEndedDialogResult } from "./game-ended-dialog.model";

@Component({
    selector: "game-ended-dialog",
    templateUrl: "./game-ended-dialog.component.html",
    styleUrls: ["./game-ended-dialog.component.scss"]
})
export class GameEndedDialog {

    result: GameEndedDialogResult = GameEndedDialogResult.doNothing;
    private colorClass: string = "";

    constructor( @Inject(MD_DIALOG_DATA) public data: GameEndedDialogData,
        public dialogRef: MdDialogRef<GameEndedDialogResult>
    ) {
        console.log(data);
        this.setColor(data.teamColor);
    }

    private setColor(color: string) {
        this.colorClass = color + "-team";
    }

    public restartGame() {
        this.result = GameEndedDialogResult.newGame;
        this.dialogRef.close(this.result);
    }

    public goToMainMenu() {
        this.result = GameEndedDialogResult.mainMenu;
        this.dialogRef.close(this.result);
    }
}