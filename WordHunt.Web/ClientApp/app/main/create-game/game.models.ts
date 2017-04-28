
export class GameCreate {
    name: string;
    userId: number;
    boardWidth: number;
    boardHeight: number;
    teamCount: number;
    trapCount: number;
    type: string;
    endMode: string;
    languageId: number;

    teams: GameTeamCreate[];
}

export class GameTeamCreate {
    name: string;
    fieldCount: number;
}