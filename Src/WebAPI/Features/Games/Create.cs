using MediatR;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Games
{
    public class Create
    {
        public class Command : IRequest<int>
        {
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IBattleshipDbContext _context;

            public Handler(IBattleshipDbContext context) => _context = context;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Game();

                _context.Games.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.GameId;
            }
        }
    }
}
