using System.Collections.Generic;

namespace Ofx.Battleship.Domain.Entities
{
    public class Game
    {
        public Game()
        {
            Boards = new HashSet<Board>();
        }

        public int GameId { get; set; }

        public ICollection<Board> Boards { get; private set; }
    }
}
