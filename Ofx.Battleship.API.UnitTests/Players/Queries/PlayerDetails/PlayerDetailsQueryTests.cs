using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Data;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Players.Details;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Players.Queries.PlayerDetails
{
    public class PlayerDetailsQueryTests : IClassFixture<DatabaseTestFixture>, IClassFixture<MappingTestFixture>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public PlayerDetailsQueryTests(DatabaseTestFixture databaseTestFixture, MappingTestFixture mappingTestFixture)
        {
            _context = databaseTestFixture.Context;
            _mapper = mappingTestFixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnPlayerDetails()
        {
            // Arrange
            var query = new Query
            {
                Id = 1
            };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.PlayerId.Should().Be(query.Id);
            response.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void Handle_GivenUnknownPlayerId_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new Query
            {
                Id = 100
            };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(query, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }
    }
}
