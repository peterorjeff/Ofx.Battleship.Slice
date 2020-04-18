using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.API.Features.Players.Create;
using Ofx.Battleship.API.Features.Players.Details;
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
            var playerId = await Mediator.Send(command);

            return Ok(playerId);
        }

        /// <summary>
        /// Get player details.
        /// </summary>
        /// <param name="id">Player ID.</param>
        /// <returns>Plater Details</returns>
        /// <response code="200">Returns a json object containing the Player details.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The player id specified could not be found.</response>
        /// <response code="500">An error has occurred.</response>
        [HttpGet("{id}")]
        [Produces(typeof(Model))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Model>> Details([FromRoute] int id)
        {
            var model = await Mediator.Send(new Query { Id = id });

            return Ok(model);
        }
    }
}
