using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Common
{
    public class IntegrationTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseStartup<TStartup>()
                .ConfigureServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<BattleshipDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>());

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    var context = scopedServices.GetRequiredService<BattleshipDbContext>();
                    context.Database.EnsureCreated();

                    var game = new Game();
                    context.Games.Add(game);
                    context.Games.Add(new Game());

                    var playerOne = new Player
                    {
                        Name = "Pete",
                        Game = game
                    };
                    var playerTwo = new Player
                    {
                        Name = "Michelle",
                        Game = game
                    };
                    context.Players.Add(playerOne);
                    context.Players.Add(playerTwo);

                    var board = new Board
                    {
                        Player = playerOne,
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
                });
        }
    }
}
