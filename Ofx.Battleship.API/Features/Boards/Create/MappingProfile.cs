using AutoMapper;
using Ofx.Battleship.API.Entities;

namespace Ofx.Battleship.API.Features.Boards.Create
{
    public class MappingProfile : Profile
    {
        public MappingProfile() =>
            CreateMap<Board, Model>(MemberList.Source);
    }
}
