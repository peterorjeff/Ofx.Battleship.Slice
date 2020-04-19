using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Data
{
    public interface IBattleshipDbContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Board> Boards { get; set; }
        DbSet<Ship> Ships { get; set; }
        DbSet<ShipPart> ShipParts { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Shot> Attack { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
