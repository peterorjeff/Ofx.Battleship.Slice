using MediatR;

namespace Ofx.Battleship.Application.Ships.Commands.AttackShip
{
    public class AttackShipCommand : IRequest<AttackViewModel>
    {
        public int BoardId { get; set; }
        public int AttackX { get; set; }
        public int AttackY { get; set; }
    }
}
