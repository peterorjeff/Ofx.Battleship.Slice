using MediatR;

namespace Ofx.Battleship.API.Features.Players.Create
{
    public class Command : IRequest<int>
    {
        public string Name { get; set; }
    }
}
