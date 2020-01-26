using AutoMapper;
using Ofx.Battleship.Application.Common.Mappings;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Games.Commands.CreateGame
{
    public class GameViewModel : IMapFrom<Game>
    {
        public int GameId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Game, GameViewModel>();
        }
    }
}
