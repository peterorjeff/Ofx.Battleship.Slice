using MediatR;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Players.Create
{
    public class Handler : IRequestHandler<Command, int>
    {
        private readonly IBattleshipDbContext _context;

        public Handler(IBattleshipDbContext context) => _context = context;

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Player
            {
                Name = request.Name
            };

            _context.Players.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.PlayerId;
        }
    }
}
