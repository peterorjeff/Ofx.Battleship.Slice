using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.UnitTests.Common
{
    /// <summary>
    /// Base class for Contructor() and Dispose() to force separate DbContext per Test.
    /// </summary>
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
