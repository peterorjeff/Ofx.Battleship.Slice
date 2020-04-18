using AutoMapper;
using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.Features.Games.Details
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, Model>();
            CreateMap<Player, PlayerModel>();
        }
    }
}
