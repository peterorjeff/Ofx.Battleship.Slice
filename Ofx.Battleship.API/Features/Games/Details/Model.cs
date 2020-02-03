using System.Collections.Generic;

namespace Ofx.Battleship.API.Features.Games.Details
{
    public class Model
    {
        public int GameId { get; set; }
        //public ICollection<BoardModel> Boards { get; set }
    }

    public class BoardModel
    {
        public int BoardId { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
    }
}
