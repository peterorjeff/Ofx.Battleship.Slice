using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using System.Threading.Tasks;

namespace Ofx.Battleship.WebAPI.Controllers
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
        [Produces(typeof(GameViewModel))]
        [ProducesResponseType(typeof(GameViewModel), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GameViewModel>> Create()
        {
            var game = await Mediator.Send(new CreateGameCommand());

            return Ok(game);
        }
    }
}
