using System.Collections.Generic;

namespace Ofx.Battleship.API.Entities
{
    public class Game
    {
        public Game()
        {
            Players = new HashSet<Player>();
        }

        public int GameId { get; set; }

        public ICollection<Player> Players { get; private set; }
    }
}
