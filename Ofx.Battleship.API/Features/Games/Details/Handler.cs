using AutoMapper;
using MediatR;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using Ofx.Battleship.API.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Games.Details
{
    public class Handler : IRequestHandler<Query, Model>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _context.Games
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            return _mapper.Map<Model>(entity);
        }
    }
}
