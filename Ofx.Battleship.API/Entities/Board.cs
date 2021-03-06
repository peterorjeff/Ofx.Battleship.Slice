using System.Collections.Generic;

namespace Ofx.Battleship.API.Entities
{
    public class Board
    {
        public Board()
        {
            Ships = new HashSet<Ship>();
        }

        public int BoardId { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }

        public Player Player { get; set; }
        public ICollection<Ship> Ships { get; private set; }
    }
}
