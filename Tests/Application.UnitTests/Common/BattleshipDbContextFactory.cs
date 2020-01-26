using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Persistence;
using System;

namespace Ofx.Battleship.Application.UnitTests.Common
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

            var game = new Game { GameId = 1 };
            context.Games.Add(game);

            var board = new Board { 
                Game = game,
                DimensionX = 10,
                DimensionY = 10
            };
            context.Boards.Add(board);

            var ship = new Ship { Board = board };
            context.Ships.Add(ship);

            context.ShipParts.AddRange(new[]
            {
                new ShipPart { Ship = ship, X = 1, Y = 1 },
                new ShipPart { Ship = ship, X = 1, Y = 2 }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(BattleshipDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
