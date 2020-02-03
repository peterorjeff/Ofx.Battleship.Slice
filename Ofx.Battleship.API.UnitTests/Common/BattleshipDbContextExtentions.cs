using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.UnitTests.Common.Builders;
using System;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public static class BattleshipDbContextExtentions
    {
        public static void AddGame(this BattleshipDbContext context, Action<GameBuilder> action)
        {
            var builder = new GameBuilder();
            action(builder);
            context.Games.Add(builder.Build());
            context.SaveChanges();
        }
        public static void AddPlayer(this BattleshipDbContext context, Action<PlayerBuilder> action)
        {
            var builder = new PlayerBuilder();
            action(builder);
            context.Players.Add(builder.Build());
            context.SaveChanges();
        }
    }
}
