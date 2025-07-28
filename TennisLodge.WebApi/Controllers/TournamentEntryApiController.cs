using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.Infrastructure.Extensions;
using TennisLodge.Web.ViewModels.TournamentEntry;

namespace TennisLodge.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class TournamentEntryApiController : ControllerBase
    {
        private readonly ITournamentEntryService tournamentEntryService;

        public TournamentEntryApiController(ITournamentEntryService tournamentEntryService)
        {
            this.tournamentEntryService = tournamentEntryService;
        }

        [HttpPost]
        public async Task<ActionResult> Join([FromBody] JoinTournamentDto model)
        {
            string? playerId = this.GetUserId();

            if (string.IsNullOrEmpty(playerId))
            {
                return Unauthorized();
            }

            bool success = await this.tournamentEntryService.JoinTournamentAsync(playerId, model.TournamentId);

            if (!success)
            {
                return BadRequest(new { message = "You are already registered for this tournament." });
            }

            return Ok(new { message = "Successfully joined the tournament." });
        }

        // Aquí luego puedes añadir Cancel y MyEntries
    }
}
