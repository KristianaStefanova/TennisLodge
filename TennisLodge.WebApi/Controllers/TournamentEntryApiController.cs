using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.Infrastructure.Extensions;
using TennisLodge.Web.ViewModels.TournamentEntry;

namespace TennisLodge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentEntryApiController : BaseExternalApiController
    {
        private readonly ITournamentEntryService tournamentEntryService;

        public TournamentEntryApiController(ITournamentEntryService tournamentEntryService)
        {
            this.tournamentEntryService = tournamentEntryService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Join")]
        public async Task<IActionResult> Join([Required] Guid tournamentId)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            bool success = await this.tournamentEntryService
                .JoinTournamentAsync(userId, tournamentId);

            if (!success)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpPost("Cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> Cancel([FromBody] JoinTournamentDto model)
        {
            if (model == null || model.TournamentId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid tournament ID." });
            }

            string? userId = this.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            bool result = await this.tournamentEntryService
                .CancelEntryAsync(userId, model.TournamentId);

            if (!result)
            {
                return BadRequest(new { message = "You are not joined to this tournament or cancellation failed." });
            }

            return this.Ok(new { message = "Tournament entry cancelled successfully." });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("MyEntries")]
        public async Task<ActionResult<IEnumerable<MyTournamentEntryViewModel>>> MyEntries()
        {
            string? userId = this.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            IEnumerable<MyTournamentEntryViewModel> entries = await this.tournamentEntryService
                .GetMyTournamentsAsync(userId);

            return this.Ok(entries);
        }
    }
}

