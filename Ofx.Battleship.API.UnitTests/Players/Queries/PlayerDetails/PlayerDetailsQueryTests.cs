﻿using AutoMapper;
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
    public class PlayerDetailsQueryTests : IDisposable, IClassFixture<MappingTestFixture>
    {
        private readonly BattleshipDbContext _context;
        private readonly IMapper _mapper;

        public PlayerDetailsQueryTests(MappingTestFixture mappingTestFixture)
        {
            _context = BattleshipDbContextFactory.Create();
            _mapper = mappingTestFixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnCorrectPlayerDetails()
        {
            // Arrange
            var query = new Query { Id = 1 };
            var handler = new Handler(_context, _mapper);
            _context.AddPlayer(player => player.WithId(1).WithName("Pete"));

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.PlayerId.Should().Be(1);
            response.Name.Should().Be("Pete");
        }

        [Fact]
        public async void Handle_GivenUnknownPlayerId_ShouldThrowNotFoundException()
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
