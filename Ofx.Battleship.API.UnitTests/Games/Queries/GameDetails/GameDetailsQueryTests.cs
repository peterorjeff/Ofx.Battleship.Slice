using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Games.Details;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Games.Queries.GameDetails
{
    public class GameDetailsQueryTests : IDisposable, IClassFixture<MappingTestFixture>
    {
        private readonly BattleshipDbContext _context;
        private readonly IMapper _mapper;

        public GameDetailsQueryTests(MappingTestFixture mappingTestFixture)
        {
            _context = BattleshipDbContextFactory.Create();
            _mapper = mappingTestFixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnGameDetails()
        {
            // Arrange
            var query = new Query { Id = 1 };
            var handler = new Handler(_context, _mapper);
            _context.AddGame(game =>
            {
                game.WithId(query.Id)
                    .WithBoard(1)
                    .WithBoard(2);
            });

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.GameId.Should().Be(query.Id);
            response.Boards.Should().NotBeNull();
            response.Boards.Count.Should().Be(2);
        }

        [Fact]
        public async void Handle_GivenUnknownGameId_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new Query { Id = 1 };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        public void Dispose()
        {
            BattleshipDbContextFactory.Destroy(_context);
        }
    }
}
