using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public class BattleshipDbContextFactory
    {
        public static BattleshipDbContext Create()
        {
            var options = new DbContextOptionsBuilder<BattleshipDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BattleshipDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(BattleshipDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
