using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.Data
{
    public class BattleshipDbContext : DbContext, IBattleshipDbContext
    {
        public BattleshipDbContext(DbContextOptions<BattleshipDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipPart> ShipParts { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Shot> Shots { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }
    }
}
