﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordHunt.Games.Repository
{
    public static class AccessQueries
    {
        //GAME
        public const string GetGameQuery = @"SELECT G.[Id], G.[UserId], G.[Name], G.[BoardWidth], G.[BoardHeight], G.[TeamCount], G.[Type], G.[WinningTeamId], GS.[CurrentTeamId]
                                            FROM Games G INNER JOIN
                                                    GameStatuses GS ON G.[Id] = GS.[GameId]       
                                            WHERE G.Id = @GameId AND GS.[Latest] = 1";

        //GAME FIELD
        public const string GetGameMapFieldsQuery = @"SELECT GameFields.[Id], [Word], [Checked], [CheckedByTeamId], [ColumnIndex], [RowIndex],
                                                    [TeamId],
                                                    [Type],
                                                    GameTeams.[Color]													
                                                    FROM GameFields
													LEFT JOIN GameTeams ON GameFields.[TeamId] = GameTeams.[Id]
                                                    WHERE GameFields.[GameId] = @GameId
                                                    ORDER BY [RowIndex], [ColumnIndex]";

        public const string GetGameFieldsQuery = @"SELECT GameFields.[Id], [Word], [Checked], [CheckedByTeamId], [ColumnIndex], [RowIndex],
                                                    CASE WHEN [Checked] = 1 THEN [TeamId] ELSE null END AS [TeamId],
                                                    CASE WHEN [Checked] = 1 THEN [Type] ELSE null END AS [Type],
                                                    CASE WHEN [Checked] = 1 THEN GameTeams.[Color] ELSE null END AS [Color]													
                                                    FROM GameFields
													LEFT JOIN GameTeams ON GameFields.[TeamId] = GameTeams.[Id]
                                                    WHERE GameFields.[GameId] = @GameId
                                                    ORDER BY [RowIndex], [ColumnIndex]";

        public const string GetSingleGameFieldQuery = @"SELECT [Id], [Word], [Checked], [CheckedByTeamId], [ColumnIndex], [RowIndex],
                                                    CASE WHEN [Checked] = 1 THEN [TeamId] ELSE null END AS [TeamId],
                                                    CASE WHEN [Checked] = 1 THEN [Type] ELSE null END AS [Type],
                                                    CASE WHEN [Checked] = 1 THEN GameTeams.[Color] ELSE null END AS [Color]	
                                                    FROM GameFields
													LEFT JOIN GameTeams ON GameFields.[TeamId] = GameTeams.[Id]
                                                    WHERE GameFields.[Id] = @FieldId";

        public const string GetFieldStateQuery = @"SELECT [TeamId], [Type], [Checked], [CheckedByTeamId] FROM GameFields WHERE [Id] = @FieldId ";

        public const string GetRemainingFieldCount = @"SELECT [RemainingFieldCount] FROM GameTeams WHERE [Id] = @TeamId";

        //GAME TEAM
        public const string GetGameTeamsQuery = @"SELECT [Id], [UserId], [Name], [Color], [Icon], [FieldCount], [RemainingFieldCount] FROM GameTeams WHERE GameId = @GameId";

        public const string GetGameFieldsForCreationQuery = @"SELECT [Id], [GameId], [UserId], [Name], [Color], [Icon], [FieldCount], [Order] FROM GameTeams WHERE GameId = @GameId";
        public const string GetFirstTeamIdQuery = @"SELECT TOP 1 [Id] FROM GameTeams WHERE [GameId] = @GameId ORDER BY [Order]";

        public const string GetNextTeamQuery = @"SELECT	GT.Id, GT.[Order], GT.[RemainingFieldCount] "
                                            + "FROM	Games G "
                                                + "INNER JOIN GameTeams GT ON G.Id = GT.GameId "
                                                + "INNER JOIN GameStatuses GS ON G.Id = GS.GameId "
                                                + "INNER JOIN GameTeams CT ON GS.CurrentTeamId = CT.Id "
                                            + "WHERE G.Id = @GameId "
                                                + "AND GS.Latest = 1 "
                                                + "AND GT.[Order] = CASE WHEN (CT.[Order]) = G.[TeamCount] THEN 1 ELSE CT.[Order] + 1 END";

        //GAME STATUS
        public const string GetCurrentGameState = @"SELECT	G.[Id] AS [GameId], G.[UserId], G.[EndMode], G.[Type], S.[CurrentTeamId], [Status] "
                                                    + "FROM	Games G INNER JOIN GameStatuses S ON G.Id = S.GameId "
                                                    + "WHERE G.[Id] = @GameId AND S.[Latest] = 1";
    }

    public static class ModificationQueries
    {

        public const string CheckField = @"UPDATE GameFields SET [Checked] = 1, [CheckedByTeamId] = @TeamId WHERE [Id] = @FieldId";

        public const string EndGame = @"UPDATE Games SET [WinningTeamId] = @WinningTeamId WHERE [Id] = @GameId;
                                        UPDATE GameStatuses SET [Latest] = 0 WHERE [GameId] = @GameId;
                                        INSERT INTO GameStatuses ([CurrentTeamId], [GameId], [Latest], [Status]) 
                                        VALUES (@WinningTeamId, @GameId, 1, @Status)";

        public const string DecrementRemainingFieldCount = @"UPDATE GameTeams SET RemainingFieldCount = RemainingFieldCount - 1 WHERE Id = @TeamId
                                                            SELECT [RemainingFieldCount] FROM GameTeams WHERE [Id] = @TeamId";
    }

    public static class CreationQueries
    {
        public const string CreateGameFieldQuery = @"INSERT INTO GameFields ([GameId], [Word], [Type], [TeamId], [ColumnIndex], [RowIndex], [Checked])
                                            VALUES (@GameId, @Word, @FieldType, @TeamId, @ColumnIndex, @RowIndex, 0)";

        public const string CreateGameQuery = @"INSERT INTO GAMES (Name, BoardWidth, BoardHeight, TeamCount, TrapCount, Type, EndMode, UserId, CreationDate, LanguageId)
                                    OUTPUT INSERTED.[Id], INSERTED.[BoardWidth], INSERTED.[BoardHeight], INSERTED.[TrapCount], INSERTED.[LanguageId]
                                    VALUES (@Name, @BoardWidth, @BoardHeight, @TeamCount, @TrapCount, @Type, @EndMode, @UserId, @CreationDate, @LanguageId)";

        public const string CreateGameBasedOnAnotherQuery = @"INSERT INTO Games ([Name], [BoardWidth], [BoardHeight], [TeamCount], [TrapCount], [Type], [UserId], [EndMode], [CreationDate], [LanguageId])
                                                            OUTPUT INSERTED.[Id], INSERTED.[BoardWidth], INSERTED.[BoardHeight], INSERTED.[TrapCount], INSERTED.[LanguageId]
                                                            SELECT G.[Name], G.[BoardWidth], G.[BoardHeight], G.[TeamCount], G.[TrapCount], G.[Type], G.[UserId], G.[EndMode], @Date, G.[LanguageId]
                                                            FROM Games G
                                                            WHERE G.Id = @GameId";

        public const string CreateGameStatusQuery = @"INSERT INTO GameStatuses ([CurrentTeamId], [GameId], [Latest], [Status])
                                    VALUES (@CurrentTeamId, @GameId, @Latest, @Status)";

        public const string CreateAndUpdateGameStatus = "UPDATE GameStatuses "
                                            + "SET[Latest] = 0 "
                                            + "WHERE[GameId] = @GameId "
                                            + "AND[Latest] = 1 "
                                            + "INSERT INTO GameStatuses([CurrentTeamId], [GameId], [Latest], [Status]) "
                                            + "OUTPUT inserted.[Id], inserted.[GameId], inserted.[CurrentTeamId], inserted.[Latest], inserted.[Status] "
                                            + "VALUES (@CurrentTeamId, @GameId, 1, @Status)";

        public const string CreateGameTeamQuery = @"INSERT INTO GameTeams ([FieldCount], [GameId], [Name], [Order], [UserId], [RemainingFieldCount], [Color], [Icon], [Active])
                                    OUTPUT INSERTED.[Id], INSERTED.[Order], INSERTED.[FieldCount]
                                    VALUES (@FieldCount, @GameId, @Name, @Order, @UserId, @RemainingFieldCount, @Color, @Icon, 1)";

        public const string CreateGameTeamsFromGameQuery = @"";
        
        public const string CreateGameMoveQuery = @"INSERT INTO [GameMoves] ([FieldId], [GameId], [TeamId], [Timestamp], [Type]) 
                                                    OUTPUT INSERTED.[Id]
                                                    VALUES (@FieldId, @GameId, @TeamId, @Timestamp, @Type)";
    }
}
