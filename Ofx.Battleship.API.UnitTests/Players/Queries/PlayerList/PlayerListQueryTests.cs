using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Features.Players.List;
using Ofx.Battleship.API.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Players.Queries.PlayerList
{
    public class PlayerListQueryTests : TestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public PlayerListQueryTests(MappingTestFixture mappingTestFixture)
        {
            _mapper = mappingTestFixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ReturnsAllPlayers()
        {
            // Arrange
            var query = new Query();
            var handler = new Handler(_context, _mapper);
            await _context.NewPlayer().SaveAsync();     // TODO: we could use a theory and helper method to create a multiple players, should we?

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Players.Should().NotBeNullOrEmpty();
            response.Players.Count.Should().Be(1);
        }
    }
}
