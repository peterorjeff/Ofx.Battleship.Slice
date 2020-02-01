using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.FixieTests.Common
{
    public class TestBase : IDisposable
    {
        protected readonly IntegrationTestWebApplicationFactory<Startup> _factory;
        protected readonly BattleshipDbContext _context;

        public TestBase(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _context = BattleshipDbContextFactory.Create();
        }

        public void Dispose()
        {
            BattleshipDbContextFactory.Destroy(_context);
        }
    }
}
