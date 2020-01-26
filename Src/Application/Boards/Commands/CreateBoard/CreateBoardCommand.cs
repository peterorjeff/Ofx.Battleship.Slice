using MediatR;

namespace Ofx.Battleship.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<BoardViewModel>
    {
        public int GameId { get; set; }
        public int DimensionX { get; set; } = 10;
        public int DimensionY { get; set; } = 10;
    }
}
