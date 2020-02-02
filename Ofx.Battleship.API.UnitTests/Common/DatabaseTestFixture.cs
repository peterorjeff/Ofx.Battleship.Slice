using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.UnitTests.Common
{
    public class DatabaseTestFixture : IDisposable
    {
        public DatabaseTestFixture()
        {
            Context = BattleshipDbContextFactory.Create();
        }

        public void Dispose()
        {
            BattleshipDbContextFactory.Destroy(Context);
        }

        public BattleshipDbContext Context { get; }
    }
}
