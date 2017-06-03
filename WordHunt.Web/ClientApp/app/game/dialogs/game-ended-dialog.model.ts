export class GameEndedDialogData {
    teamName: string;
    teamColor: string;
    canRestart: boolean;
}

export enum GameEndedDialogResult {
    doNothing = 0,
    newGame,
    mainMenu
}