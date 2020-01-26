using AutoMapper;
using Ofx.Battleship.Application.Common.Mappings;
using Ofx.Battleship.Domain.Entities;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class ShipViewModel : IMapFrom<Ship>
    {
        public int ShipId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ship, ShipViewModel>();
        }
    }
}
