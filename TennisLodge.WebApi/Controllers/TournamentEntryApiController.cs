using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.TournamentEntry;
using TennisLodge.Web.Infrastructure.Extensions;

namespace TennisLodge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentEntryApiController : ControllerBase
    {

        private readonly ITournamentEntryService tournamentEntryService;

        public TournamentEntryApiController(ITournamentEntryService tournamentEntryService)
        {
            this.tournamentEntryService = tournamentEntryService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Join")]
        public async Task<ActionResult<object>> Join([FromBody] JoinTournamentDto model)
        {
            string? playerId = this.GetUserId();

            if (string.IsNullOrEmpty(playerId))
            {
                return Unauthorized();
            }

            bool success = await this.tournamentEntryService
                .JoinTournamentAsync(playerId, model.TournamentId);

            if (!success)
            {
                return BadRequest(new { message = "You are already registered for this tournament." });
            }

            return this.Ok(new { message = "Successfully joined the tournament." });
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
            string userId = this.GetUserId();

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

