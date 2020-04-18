using Ofx.Battleship.API.ServerTests.Infrastructure;
using Ofx.Battleship.API.ServerTests.Records;

namespace Ofx.Battleship.API.ServerTests.Common
{
    public static class ServerExtensions
    {
        public static BoardRecord NewBoard(this Server server) => new BoardRecord(server.DbContext);

        public static GameRecord NewGame(this Server server) => new GameRecord(server.DbContext);

        public static PlayerRecord NewPlayer(this Server server) => new PlayerRecord(server.DbContext);

        public static ShipPartRecord NewShipPart(this Server server) => new ShipPartRecord(server.DbContext);

        public static ShipRecord NewShip(this Server server) => new ShipRecord(server.DbContext);
    }
}
