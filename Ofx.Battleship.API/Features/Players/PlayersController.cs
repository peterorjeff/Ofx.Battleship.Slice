using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.API.Features.Players.Create;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Players
{
    public class PlayersController : BaseController
    {
        /// <summary>
        /// Create a new Player.
        /// </summary>
        /// <param name="command">Create Player command where player name should be specified.</param>
        /// <returns>New player id.</returns>
        /// <response code="200">Returns a json object containing the id of the new Player.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="500">An error has occurred.</response>
        [HttpPost]
        [Produces(typeof(int))]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<int>> Create([FromBody] Command command)
        {
            var gameId = await Mediator.Send(command);

            return Ok(gameId);
        }

    }
}
