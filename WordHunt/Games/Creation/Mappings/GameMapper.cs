﻿using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Games.Creation.Mappings
{
    public interface IGameMapper
    {
        Game MapGame(GameCreate model);
        GameTeam MapGameTeam(GameTeamCreate model);
    }

    public class GameMapper : IGameMapper
    {
        public GameTeam MapGameTeam(GameTeamCreate model)
        {
            return new GameTeam()
            {
                UserId = model.UserId,
                Name = model.Name,
                FieldCount = model.FieldCount,
                RemainingFieldCount = model.FieldCount,
                Order = model.Order,
                GameId = model.GameId,
            };
        }

        public Game MapGame(GameCreate model)
        {
            return new Game()
            {
               BoardHeight = model.BoardHeight,
               BoardWidth = model.BoardWidth,
               EndMode = model.EndMode,
               Name = model.Name,
               TeamCount = model.TeamCount,
               TrapCount = model.TrapCount,
               Type = model.Type,
               UserId = model.UserId
            };
        }
    }
}