using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using Ofx.Battleship.Application.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Games.Commands.CreateGame
{
    public class CreateGameCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateGameCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnGameViewModelWithNewGameId()
        {
            // Arrange
            var command = new CreateGameCommandHandler(_context, _mapper);

            // Act
            var response = await command.Handle(new CreateGameCommand(), CancellationToken.None);

            // Assert
            response.Should().BeOfType<GameViewModel>();
            response.GameId.Should().BePositive();
        }
    }
}
