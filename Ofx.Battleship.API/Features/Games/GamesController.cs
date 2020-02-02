using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.API.Features.Games.Create;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Games
{
    public class GamesController : BaseController
    {
        /// <summary>
        /// Create a new Game of Battleship.
        /// </summary>
        /// <returns>The new GameId.</returns>
        /// <response code="200">Returns a json object containing the id for the new Game.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(int))]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<int>> Create()
        {
            var gameId = await Mediator.Send(new Command());

            return Ok(gameId);
        }
    }
}
