using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Base.Enums.Game;
using WordHunt.Games.Mappings;
using Xunit;

namespace WordHunt.UnitTests.Mappings
{
    public class GameCreateMappingTests
    {
        [Fact]
        public void ShouldReturnValidGameStatusObject()
        {
            var mapper = new GameMapper();
            var gameId = 1;
            var teamId = 1;

            var status = mapper.MapStatus(gameId, teamId);

            Assert.Equal(gameId, status.GameId);
            Assert.Equal(gameId, status.CurrentTeamId);
            Assert.Equal(true, status.Latest);
            Assert.Equal(Status.Created, status.Status);
        }
    }
}
