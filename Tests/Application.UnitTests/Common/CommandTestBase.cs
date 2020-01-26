using Ofx.Battleship.Persistence;
using System;

namespace Ofx.Battleship.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly BattleshipDbContext _context;

        public CommandTestBase()
        {
            _context = BattleshipDbContextFactory.Create();
        }

        public void Dispose()
        {
            BattleshipDbContextFactory.Destroy(_context);
        }
    }
}
