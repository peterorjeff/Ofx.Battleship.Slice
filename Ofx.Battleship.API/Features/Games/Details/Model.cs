using System.Collections.Generic;

namespace Ofx.Battleship.API.Features.Games.Details
{
    public class Model
    {
        public int GameId { get; set; }

        public ICollection<PlayerModel> Boards { get; set; }
    }

    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
    }
}
