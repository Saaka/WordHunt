﻿using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Base.Services;
using WordHunt.Data.Connection;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Repository
{
    public interface IGameRepository
    {
        Task<GameCreated> SaveNewGame(GameCreate game);
        Task<Models.Games.Access.Game> GetCompleteGameInfo(int gameId);
        Task<Models.Games.Access.CurrentGameState> GetCurrentGameState(int gameId);
        Task EndGame(int gameId, int winningTeamId);
        Task<Models.Games.Access.Game> GetCompleteGameMap(int gameId);
        Task<GameCreated> CreatedBasedOnGame(int gameId);
    }

    class GameRepository : IGameRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ITimeProvider timeProvider;
        private readonly IMapper mapper;

        public GameRepository(IDbConnectionFactory connectionFactory,
            ITimeProvider timeProvider,
            IMapper mapper)
        {
            this.connectionFactory = connectionFactory;
            this.timeProvider = timeProvider;
            this.mapper = mapper;
        }

        public async Task<GameCreated> SaveNewGame(GameCreate model)
        {
            var entity = mapper.Map<Data.Entities.Game>(model);
            entity.CreationDate = timeProvider.GetCurrentTime();

            using (var connection = connectionFactory.CreateConnection())
            {
                var game = await connection.QueryFirstAsync<GameCreated>(CreationQueries.CreateGameQuery, entity);

                return game;
            }
        }

        public async Task<GameCreated> CreatedBasedOnGame(int gameId)
        {
            var creationDate = timeProvider.GetCurrentTime();
            using (var connection = connectionFactory.CreateConnection())
            {
                var game = await connection.QueryFirstAsync<GameCreated>(CreationQueries.CreateGameBasedOnAnotherQuery, new { GameId = gameId, Date = creationDate });

                return game;
            }
        }

        public async Task<Models.Games.Access.Game> GetCompleteGameMap(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine(AccessQueries.GetGameQuery);
                query.AppendLine(AccessQueries.GetGameMapFieldsQuery);
                query.AppendLine(AccessQueries.GetGameTeamsQuery);
                using (var multiQuery = await connection.QueryMultipleAsync(query.ToString(), new { GameId = gameId }))
                {
                    var game = await multiQuery.ReadFirstAsync<Models.Games.Access.Game>();
                    game.Fields = await multiQuery.ReadAsync<Models.Games.Access.BoardField>();
                    game.Teams = await multiQuery.ReadAsync<Models.Games.Access.Team>();

                    return game;
                }
            }
        }

        public async Task<Models.Games.Access.Game> GetCompleteGameInfo(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine(AccessQueries.GetGameQuery);
                query.AppendLine(AccessQueries.GetGameFieldsQuery);
                query.AppendLine(AccessQueries.GetGameTeamsQuery);
                using (var multiQuery = await connection.QueryMultipleAsync(query.ToString(), new { GameId = gameId }))
                {
                    var game = await multiQuery.ReadFirstAsync<Models.Games.Access.Game>();
                    game.Fields = await multiQuery.ReadAsync<Models.Games.Access.BoardField>();
                    game.Teams = await multiQuery.ReadAsync<Models.Games.Access.Team>();

                    return game;
                }
            }
        }

        public async Task<Models.Games.Access.CurrentGameState> GetCurrentGameState(int gameId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return await connection.QueryFirstAsync<Models.Games.Access.CurrentGameState>(AccessQueries.GetCurrentGameState, new { GameId = gameId });
            }
        }

        public async Task EndGame(int gameId, int winningTeamId)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(ModificationQueries.EndGame, new { GameId = gameId, WinningTeamId = winningTeamId, Status = (int)Base.Enums.Game.Status.Finished });
            }
        }
    }
}
