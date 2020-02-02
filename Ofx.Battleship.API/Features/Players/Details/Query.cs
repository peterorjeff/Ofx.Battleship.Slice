using MediatR;

namespace Ofx.Battleship.API.Features.Players.Details
{
    public class Query : IRequest<Model>
    {
        public int Id { get; set; }
    }
}
