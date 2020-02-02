using MediatR;
using Ofx.Battleship.API.Enums;

namespace Ofx.Battleship.API.Features.Ships.Create
{
    public class Command : IRequest<int>
    {
        public int BoardId { get; set; }
        public int BowX { get; set; }
        public int BowY { get; set; }
        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }
    }
}
