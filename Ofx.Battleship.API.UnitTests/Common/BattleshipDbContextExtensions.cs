using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Test.Common.Records;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public static class BattleshipDbContextExtensions
    {
        public static GameRecord NewGame(this BattleshipDbContext context) => new GameRecord(context);

        public static PlayerRecord NewPlayer(this BattleshipDbContext context) => new PlayerRecord(context);

        public static BoardRecord NewBoard(this BattleshipDbContext context) => new BoardRecord(context);

        public static ShipPartRecord NewShipPart(this BattleshipDbContext context) => new ShipPartRecord(context);

        public static ShipRecord NewShip(this BattleshipDbContext context) => new ShipRecord(context);
    }
}
