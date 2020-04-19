using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Players.List
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
            var players = await _context.Players.ToListAsync();

            return _mapper.Map<List<Player>, Model>(players);
        }
    }
}
