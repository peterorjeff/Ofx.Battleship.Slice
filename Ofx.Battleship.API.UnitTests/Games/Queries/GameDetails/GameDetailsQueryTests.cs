using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Games.Details;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Games.Queries.GameDetails
{
    public class GameDetailsQueryTests : TestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public GameDetailsQueryTests(MappingTestFixture mappingTestFixture)
        {
            _mapper = mappingTestFixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnGameDetails()
        {
            // Arrange
            var query = new Query { Id = 1 };
            var handler = new Handler(_context, _mapper);
            await _context.NewGame().WithGameId(query.Id).SaveAsync();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.GameId.Should().Be(query.Id);
            response.Players.Should().NotBeNull();
            response.Players.Count.Should().Be(0);
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
    }
}
