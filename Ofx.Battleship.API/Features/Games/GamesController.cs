using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.API.Features.Games.Create;
using Ofx.Battleship.API.Features.Games.Details;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Create()
        {
            var gameId = await Mediator.Send(new Command());

            return Ok(gameId);
        }

        /// <summary>
        /// Get game details.
        /// </summary>
        /// <param name="id">Game ID.</param>
        /// <returns>Plater Details</returns>
        /// <response code="200">Returns a json object containing the Game details.</response>
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
