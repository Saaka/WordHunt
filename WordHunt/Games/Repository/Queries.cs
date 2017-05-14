using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Games.Repository
{
    public static class AccessQueries
    {
        //GAME
        public const string GetGameQuery = @"SELECT [Id], [UserId], [Name], [BoardWidth], [BoardHeight], [TeamCount], [Type] FROM Games WHERE Id = @GameId";

        //GAME FIELD
        public const string GetGameFieldsQuery = @"SELECT [Id], [Word], [Checked], [CheckedByTeamId], [ColumnIndex], [RowIndex],
                                                    CASE WHEN [CheckedByTeamId] = [TeamId] AND [Checked] = 1 THEN 1 ELSE 0 END AS [CheckedByRightTeam]
                                                    FROM GameFields
                                                    WHERE [GameId] = @GameId
                                                    ORDER BY [RowIndex], [ColumnIndex]";

        //GAME TEAM
        public const string GetGameTeamsQuery = @"SELECT [Id], [UserId], [Name], [Color], [Icon], [FieldCount], [RemainingFieldCount] FROM GameTeams WHERE GameId = @GameId";
        public const string GetFirstTeamIdQuery = @"SELECT TOP 1 [Id] FROM GameTeams WHERE [GameId] = @GameId ORDER BY [Order]";

        //GAME STATUS
        public const string GetCurrentGameState = @"SELECT	G.[UserId], G.[EndMode], G.[Type], S.[CurrentTeamId], [Status] "
                                                    + "FROM	Games G INNER JOIN GameStatuses S ON G.Id = S.GameId "
                                                    + "WHERE	G.[Id] = @GameId AND S.[Latest] = 1";
    }

    public static class CreationQueries
    {
        public const string CreateGameFieldQuery = @"INSERT INTO GameFields ([GameId], [Word], [Type], [TeamId], [ColumnIndex], [RowIndex], [Checked])
                                            VALUES (@GameId, @Word, @FieldType, @TeamId, @ColumnIndex, @RowIndex, 0)";

        public const string CreateGameQuery = @"INSERT INTO GAMES (Name, BoardWidth, BoardHeight, TeamCount, TrapCount, Type, EndMode, UserId, CreationDate, LanguageId)
                                    OUTPUT INSERTED.[Id], INSERTED.[BoardWidth], INSERTED.[BoardHeight], INSERTED.[TrapCount], INSERTED.[LanguageId]
                                    VALUES (@Name, @BoardWidth, @BoardHeight, @TeamCount, @TrapCount, @Type, @EndMode, @UserId, @CreationDate, @LanguageId)";

        public const string CreateGameStatusQuery = @"INSERT INTO GameStatuses ([CurrentTeamId], [GameId], [Latest], [Status])
                                    VALUES (@CurrentTeamId, @GameId, @Latest, @Status)";

        public const string CreateGameTeamQuery = @"INSERT INTO GameTeams ([FieldCount], [GameId], [Name], [Order], [UserId], [RemainingFieldCount])
                                    OUTPUT INSERTED.[Id], INSERTED.[Order], INSERTED.[FieldCount]
                                    VALUES (@FieldCount, @GameId, @Name, @Order, @UserId, @RemainingFieldCount)";
    }
}
