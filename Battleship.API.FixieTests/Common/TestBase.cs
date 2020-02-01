using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.FixieTests.Common
{
    public class TestBase : IDisposable
    {
        protected readonly BattleshipDbContext _context;

        public TestBase()
        {
            _context = BattleshipDbContextFactory.Create();
        }

        public void Dispose()
        {
            BattleshipDbContextFactory.Destroy(_context);
        }
    }
}
