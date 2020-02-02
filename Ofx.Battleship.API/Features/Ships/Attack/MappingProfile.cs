using AutoMapper;
using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.Features.Ships.Attack
{
    public class MappingProfile : Profile
    {
        public MappingProfile() =>
            CreateMap<ShipPart, Model>(MemberList.Source);
    }
}
