using MediatR;
using Ofx.Battleship.Domain.Enums;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommand : IRequest<ShipViewModel>
    {
        public int BoardId { get; set; }
        public int BowX { get; set; }
        public int BowY { get; set; }
        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }
    }
}
