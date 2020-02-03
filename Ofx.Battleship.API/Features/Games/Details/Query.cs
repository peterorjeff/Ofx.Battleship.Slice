using MediatR;

namespace Ofx.Battleship.API.Features.Games.Details
{
    public class Query : IRequest<Model>
    {
        public int Id { get; set; }
    }
}
