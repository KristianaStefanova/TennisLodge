using System.ComponentModel.DataAnnotations;
using static TennisLodge.Data.Common.EntityConstants.Accommodation;
using static TennisLodge.Web.ViewModels.ValidationMessages.Accommodation;

namespace TennisLodge.Web.ViewModels.Accommodation
{
    public class AccommodationCreateInputModel
    {
        [Required(ErrorMessage = CityRequiredMessage)]
        [MinLength(CityMinLength, ErrorMessage = CityMinLengthMessage)]
        [MaxLength(CityMaxLength, ErrorMessage = CityMaxLengthMessage)]
        public string City { get; set; } = null!;


        [Required(ErrorMessage = AddressRequiredMessage)]
        [MinLength(AddressMinLength, ErrorMessage = AddressMinLengthMessage)]
        [MaxLength(AddressMaxLength, ErrorMessage = AddressMaxLengthMessage)]
        public string Address { get; set; } = null!;


        [Required(ErrorMessage = MaxGuestsRequiredMessage)]
        [Range(MinGuestsCount, MaxGuestsCount, ErrorMessage = MaxGuestsRangeMessage)]
        public int MaxGuests { get; set; }


        [Required(ErrorMessage = AvailableFromDateInvalidMessage)]
        [DataType(DataType.Date)]
        [Display(Name = "Available From")]
        public DateTime? AvailableFrom { get; set; }


        [Required(ErrorMessage = AvailableToDateInvalidMessage)]
        [DataType(DataType.Date)]
        [Display(Name = "Available To")]
        public DateTime? AvailableTo { get; set; }


        [StringLength(NotesMaxLength, ErrorMessage = NotesMaxLengthMessage)]
        public string? Notes { get; set; }
    }
}



