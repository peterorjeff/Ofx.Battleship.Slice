using MediatR;

namespace Ofx.Battleship.API.Features.Ships.Attack
{
    public class Command : IRequest<Model>
    {
        public int BoardId { get; set; }
        public int AttackX { get; set; }
        public int AttackY { get; set; }
    }

}
