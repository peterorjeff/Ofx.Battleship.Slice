using FluentAssertions;
using Ofx.Battleship.API.Features.Players.Create;
using Ofx.Battleship.API.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Ofx.Battleship.API.UnitTests.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandTests : CommandTestBase
    {
        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnNewPlayerId()
        {
            // Arrange
            var command = new Handler(_context);

            // Act
            var response = await command.Handle(new Command(), CancellationToken.None);

            // Assert
            response.Should().BePositive();
        }
    }
}
