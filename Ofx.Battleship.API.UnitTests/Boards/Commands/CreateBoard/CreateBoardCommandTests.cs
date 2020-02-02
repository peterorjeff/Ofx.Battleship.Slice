using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Exceptions;
using Ofx.Battleship.API.Features.Boards.Create;
using Ofx.Battleship.API.UnitTests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateBoardCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenUnknownGameId_ShouldThrowNotFoundException()
        {   
            // Arrange
            var command = new Command { GameId = 100 };
            var handler = new Handler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownGameId_ShouldReturnNewBoardId()
        {
            // Arrange
            var command = new Command { GameId = 1 };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.BoardId.Should().BePositive();
        }

        [Fact]
        public async void Handle_GivenKnownGameId_ShouldReturnBoardViewModel()
        {
            // Arrange
            var command = new Command { GameId = 1 };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<Model>();
        }

        [Fact]
        public async void Handle_GivenRequestWithoutDimensions_ShouldReturnBoardWithDefaultDimensions()
        {
            // Arrange
            var command = new Command { GameId = 1 };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.DimensionX.Should().Be(10);
            response.DimensionY.Should().Be(10);
        }

        [Fact]
        public async void Handle_GivenRequestWithDimensions_ShouldReturnBoardWithSpecifiedDimensions()
        {
            // Arrange
            var command = new Command 
            { 
                GameId = 1,
                DimensionX = 8,
                DimensionY = 12
            };
            var handler = new Handler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.DimensionX.Should().Be(8);
            response.DimensionY.Should().Be(12);
        }
    }
}
