using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.API.Features.Games;
using Ofx.Battleship.API.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Games.Commands.CreateGame
{
    public class CreateGameCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateGameCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnNewGameId()
        {
            // Arrange
            var command = new Create.Handler(_context);

            // Act
            var response = await command.Handle(new Create.Command(), CancellationToken.None);

            // Assert
            response.Should().BePositive();
        }
    }
}
