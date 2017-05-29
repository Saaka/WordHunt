
export class Game {
    id: number;
    name: string;
    userId: number;
    boardWidth: number;
    boardHeight: number;
    teamCount: number;
    type: string;
    currentTeamId: number;

    teams: Team[];
    fields: Field[];
}

export class Team {
    id: number;
    name: string;
    userId: number;
    color: string;
    icon: string;
    fieldCount: number;
    remainingFieldCount: number;
}

export class Field {
    id: number;
    word: string;
    checked: boolean;
    teamId: number;
    checkedByTeamId: number;
    type: number;
    columnIndex: number;
    rowIndex: number;
    color: string;
}

export class TeamChanged {
    gameId: number;
    newTeamId: number;
    lastTeamId: number;
    changeReason: number;
    remainingFieldCount: number;
}

export class FieldChecked {
    gameId: number;
    fieldId: number;
    teamId: number;
    type: number;
    checked: boolean;
}

export class GameEnded {
    gameId: number;
    winningTeamId: number;
}