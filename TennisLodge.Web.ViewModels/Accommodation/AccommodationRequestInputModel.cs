using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static TennisLodge.Data.Common.EntityConstants.AccommodationRequest;
using static TennisLodge.Web.ViewModels.ValidationMessages.AccommodationRequest;

namespace TennisLodge.Web.ViewModels.Accommodation
{
    public class AccommodationRequestInputModel
    {
        [Required]
        public Guid AccommodationId { get; set; }


        [Required(ErrorMessage = TournamentIdRequiredMessage)]
        public string TournamentId { get; set; } = null!;


        [Range(NumberOfGuestsMinValue, NumberOfGuestsMaxValue, ErrorMessage = NumberOfGuestsRangeMessage)]
        public int NumberOfGuests { get; set; }


        [MaxLength(NotesdMaxLength, ErrorMessage = NotesMaxLengthMessage)]
        public string? Notes { get; set; }

        public IEnumerable<SelectListItem> Tournaments { get; set; } = new List<SelectListItem>();
    }
}
