using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.Application.Common.Interfaces
{
    public interface IBattleshipDbContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Board> Boards { get; set; }
        DbSet<Ship> Ships { get; set; }
        DbSet<ShipPart> ShipParts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
