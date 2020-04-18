using MediatR;

namespace Ofx.Battleship.API.Features.Boards.Create
{
    public class Command : IRequest<Model>
    {
        public int PlayerId { get; set; }
        public int DimensionX { get; set; } = 10;
        public int DimensionY { get; set; } = 10;
    }
}
