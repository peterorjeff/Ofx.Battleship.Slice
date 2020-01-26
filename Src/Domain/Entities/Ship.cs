using System.Collections.Generic;

namespace Ofx.Battleship.Domain.Entities
{
    public class Ship
    {
        public Ship()
        {
            ShipParts = new HashSet<ShipPart>();
        }

        public int ShipId { get; set; }
        
        public Board Board { get; set; }
        public ICollection<ShipPart> ShipParts { get; private set; }
    }
}
