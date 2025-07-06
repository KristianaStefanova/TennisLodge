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
    }
}
