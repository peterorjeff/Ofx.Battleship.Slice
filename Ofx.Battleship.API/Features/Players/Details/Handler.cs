using AutoMapper;
using MediatR;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Players.Details
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
            var entity = await _context.Players
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Players), request.Id);
            }

            return _mapper.Map<Model>(entity);
        }
    }
}
