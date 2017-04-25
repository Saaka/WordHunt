
export class GameCreate {
    name: string;
    userId: number;
    boardWidth: number;
    boardHeight: number;
    teamCount: number;
    trapCount: number;
    type: string;
    endMode: string;

    teams: GameTeamCreate[];
}

export class GameTeamCreate {
    name: string;
    fieldCount: number;
}