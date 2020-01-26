using AutoMapper;
using Ofx.Battleship.Application.Common.Mappings;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Boards.Commands.CreateBoard
{
    public class BoardViewModel : IMapFrom<Board>
    {
        public int BoardId { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Board, BoardViewModel>();
        }
    }
}
