using MediatR;

namespace Ofx.Battleship.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<GameViewModel>
    {
    }
}
