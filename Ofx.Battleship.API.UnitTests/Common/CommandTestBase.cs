﻿using Ofx.Battleship.API.Data;
using System;

namespace Ofx.Battleship.API.UnitTests.Common
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