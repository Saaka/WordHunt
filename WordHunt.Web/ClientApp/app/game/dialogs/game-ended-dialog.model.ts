export class GameEndedDialogData {
    teamName: string;
    canRestart: boolean;
}

export enum GameEndedDialogResult {
    newGame = 1,
    mainMenu,
    doNothing
}