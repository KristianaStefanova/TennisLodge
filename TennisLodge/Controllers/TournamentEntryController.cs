using Microsoft.AspNetCore.Mvc;
using TennisLodge.Data.Models;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.TournamentEntry;

namespace TennisLodge.Web.Controllers
{
    public class TournamentEntryController : BaseController
    {
        private readonly ITournamentEntryService tournamentEntryService;

        public TournamentEntryController(ITournamentEntryService tournamentEntryService)
        {
            this.tournamentEntryService = tournamentEntryService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Join(Guid id)
        {
            try
            {
                string? playerId = this.GetUserId();

                if (string.IsNullOrEmpty(playerId))
                {
                    return Unauthorized();
                }

                bool success = await tournamentEntryService
                    .JoinTournamentAsync(playerId, id);

                if (!success)
                {
                    return BadRequest("You are already registered for this tournament.");
                }

                return RedirectToAction("Details", "Tournament", new { id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "An error occurred while registering for the tournament.");
            }
        }

        public async Task<IActionResult> MyTournaments()
        {
            try
            {
                string? playerId = this.GetUserId();

                if (string.IsNullOrEmpty(playerId))
                {
                    return Unauthorized();
                }

                IEnumerable<MyTournamentEntryViewModel> entries = await tournamentEntryService
                    .GetMyTournamentsAsync(playerId);

                return View(entries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "Error getting your tournaments.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(Guid tournamentId)
        {
            try
            {
                string? playerId = this.GetUserId();

                if (string.IsNullOrEmpty(playerId))
                {
                    return Unauthorized();
                }

                bool success = await tournamentEntryService
                    .CancelEntryAsync(playerId, tournamentId);

                if (!success)
                {
                    return BadRequest("The registration could not be canceled.");
                }

                return RedirectToAction(nameof(MyTournaments));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, "Error canceling registration.");
            }
        }

    }
}

