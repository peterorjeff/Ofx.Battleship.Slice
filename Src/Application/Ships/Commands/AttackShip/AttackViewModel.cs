using AutoMapper;
using Ofx.Battleship.Application.Common.Mappings;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Ships.Commands.AttackShip
{
    public class AttackViewModel : IMapFrom<ShipPart>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Hit { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipPart, AttackViewModel>();
        }
    }
}
