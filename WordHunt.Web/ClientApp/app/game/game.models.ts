
export class Game {
    id: number;
    name: string;
    userId: number;
    boardWidth: number;
    boardHeight: number;
    teamCount: number;
    type: string;

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
    checkedByRightTeam: boolean;
    checkedByTeamId: number;
    columnIndex: number;
    rowIndex: number;
}

export class TeamChanged {
    gameId: number;
    newTeamId: number;
    lastTeamId: number;
    changeReason: number;
}