using AutoMapper;
using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.Features.Players.Details
{
    public class MappingProfile : Profile
    {
        public MappingProfile() =>
            CreateMap<Player, Model>(MemberList.Source);
    }
}
