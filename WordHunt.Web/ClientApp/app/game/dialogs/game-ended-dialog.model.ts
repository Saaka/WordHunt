export class GameEndedDialogData {
    teamName: string;
    teamColor: string;
    canRestart: boolean;
}

export enum GameEndedDialogResult {
    newGame = 1,
    mainMenu,
    doNothing
}