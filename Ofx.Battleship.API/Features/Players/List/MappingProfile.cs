using AutoMapper;
using Ofx.Battleship.API.Entities;
using System.Collections.Generic;

namespace Ofx.Battleship.API.Features.Players.List
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<List<Player>, Model>(MemberList.None)
                .ForMember(destination => destination.Players, opt => opt.MapFrom(souce => souce));
            CreateMap<Player, PlayerModel>(MemberList.Source);
        }
    }
}
