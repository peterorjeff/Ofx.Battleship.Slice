﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ofx.Battleship.API.Features.Boards
{
    public class BoardsController : BaseController
    {
        /// <summary>
        /// Create a new Board for an existing Game of Battleship.
        /// </summary>
        /// <param name="command">Create board command where the GameId should be specified. Optionally the board dimensions can be specified.</param>
        /// <returns>The new BoardId.</returns>
        /// <response code="200">Returns a json object containing the id of the new Board.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The Game could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(Create.Model))]
        [ProducesResponseType(typeof(Create.Model), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Create.Model>> Create([FromBody] Create.Command command)
        {
            var board = await Mediator.Send(command);

            return Ok(board);
        }
    }
}
