
using System.ComponentModel.DataAnnotations;

using static TennisLodge.Data.Common.EntityConstants.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;
using static TennisLodge.Web.ViewModels.ValidationMessages.Tournament;


namespace TennisLodge.Web.ViewModels.Tournament
{
    public class TournamentFormInputModel
    {
        public string Id { get; set; } = string.Empty;


        [Required(ErrorMessage = NameRequiredMessage)]
        [MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = LocationRequiredMessage)]
        [MinLength(LocationMinLength, ErrorMessage = LocationMinLengthMessage)]
        [MaxLength(LocationMaxLength, ErrorMessage = LocationMaxLengthMessage)]
        public string Location { get; set; } = null!;


        [Required(ErrorMessage = SurfaceRequiredMessage)]
        [MinLength(SurfaceMinLength, ErrorMessage = SurfaceMinLengthMessage)]
        [MaxLength(SurfaceMaxLength, ErrorMessage = SurfaceMaxLengthMessage)]
        public string Surface { get; set; } = null!;


        [Required(ErrorMessage = CategoryRequiredMessage)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = OrganizerRequiredMessage)]
        [MinLength(OrganizerNameMinLength, ErrorMessage = OrganizerNameMinLengthMessage)]
        [MaxLength(OrganizerNameMaxLength, ErrorMessage = OrganizerNameMaxLengthMessage)]
        public string Organizer { get; set; } = null!;


        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
        public string? ImageUrl { get; set; } = $"/images/{NoImageUrl}";


        [Required(ErrorMessage = StartDateRequiredMessage)]
        public string StartDate { get; set; } = null!;


        [Required(ErrorMessage = EndDateRequiredMessage)]
        public string EndDate { get; set; } = null!;

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    }
}
