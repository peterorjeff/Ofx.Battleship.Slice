using AutoMapper;
using MediatR;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Common.Interfaces;
using Ofx.Battleship.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public CreateBoardCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BoardViewModel> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.GameId);
            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            var entity = new Board
            {
                Game = game,
                DimensionX = request.DimensionX,
                DimensionY = request.DimensionY,
            };

            _context.Boards.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<BoardViewModel>(entity);

            return viewModel;
        }
    }
}
