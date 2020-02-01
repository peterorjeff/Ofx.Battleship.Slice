using FluentAssertions;
using Ofx.Battleship.API.Features.Games;
using Ofx.Battleship.API.FixieTests.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.API.FixieTests.Features
{
    public class CreateGameTests : TestBase
    {
        public async Task Handle_GivenValidRequest_ShouldReturnNewGameId()
        {
            // Arrange
            var handler = new Create.Handler(_context);

            // Act
            var response = await handler.Handle(new Create.Command(), CancellationToken.None);

            // Assert
            response.Should().BePositive();
        }
    }
}
