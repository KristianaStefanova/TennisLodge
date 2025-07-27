using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Web.ViewModels
{
    public static class ValidationMessages
    {
        public static class Tournament
        {

            // Error messages
            public const string NameRequiredMessage = "Name is required.";
            public const string NameMinLengthMessage = "Name must be at least 2 characters.";
            public const string NameMaxLengthMessage = "Name cannot exceed 100 characters.";

            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 1000 characters.";

            public const string LocationRequiredMessage = "Location is required.";
            public const string LocationMinLengthMessage = "Location must be at least 3 characters.";
            public const string LocationMaxLengthMessage = "Location cannot exceed 50 characters.";

            public const string OrganizerRequiredMessage = "Organizer is required.";
            public const string OrganizerNameMinLengthMessage = "Organizer name must be at least 2 characters.";
            public const string OrganizerNameMaxLengthMessage = "Organizer name cannot exceed 50 characters.";

            public const string SurfaceRequiredMessage = "Surface is required.";
            public const string SurfaceMinLengthMessage = "Surface must be at least 3 characters.";
            public const string SurfaceMaxLengthMessage = "Surface cannot exceed 30 characters.";

            public const string CategoryRequiredMessage = "Category is required.";
            public const string CategoryMinLengthMessage = "Category must be at least 3 characters.";
            public const string CategoryMaxLengthMessage = "Category cannot exceed 30 characters.";

            public const string StartDateRequiredMessage = "Start date is required.";
            public const string EndDateRequiredMessage = "End date is required.";

            public const string ImageUrlMaxLengthMessage = "Image URL cannot exceed 2048 characters.";

            public const string ServiceCreateError =
                "Fatal error occurred while adding your tournament! Please try again later!";
        }

        public static class Accommodation
        {
            public const string CityRequiredMessage = "City is required.";
            public const string CityMinLengthMessage = "City must be at least 2 characters.";
            public const string CityMaxLengthMessage = "City cannot exceed 50 characters.";
            public const string AddressRequiredMessage = "Address is required.";
            public const string AddressMinLengthMessage = "Address must be at least 2 characters.";
            public const string AddressMaxLengthMessage = "Address cannot exceed 200 characters.";
            public const string MaxGuestsRequiredMessage = "Maximum number of guests is required.";
            public const string MaxGuestsRangeMessage = "Maximum number of guests must be between 1 and 20.";
            public const string AvailableFromDateInvalidMessage = "Available From date is invalid.";
            public const string AvailableToDateInvalidMessage = "Available To date is invalid.";
            public const string AvailableToBeforeFromDateMessage = "Available To date must be after Available From date.";
            public const string NotesMaxLengthMessage = "Notes cannot exceed 500 characters.";
        }

        public static class AccommodationRequest
        {
            public const string TournamentIdRequiredMessage = "Tournament ID is required.";
            public const string NumberOfGuestsRangeMessage = "Number of guests must be between 1 and 10.";
            public const string NotesMaxLengthMessage = "Notes cannot exceed 500 characters.";
        }
    }
}
