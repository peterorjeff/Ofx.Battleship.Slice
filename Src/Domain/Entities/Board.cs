using System.Collections.Generic;

namespace Ofx.Battleship.Domain.Entities
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

        public Game Game { get; set; }
        public ICollection<Ship> Ships { get; private set; }
    }
}
