using System.Collections.Generic;

namespace Ofx.Battleship.API.Features.Players.List
{
    public class Model
    {
        public IList<PlayerModel> Players { get; set; }
    }

    public class PlayerModel
    { 
        public int PlayerId { get; set; }
        public string Name { get; set; }
    }
}
