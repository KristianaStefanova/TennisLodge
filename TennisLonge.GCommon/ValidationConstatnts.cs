using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.GCommon
{
    public static class ValidationConstatnts
    {
        public static class Tournament
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;

            public const int LocationMaxLength = 150;

            public const int SurfaceMaxLength = 50;

            public const int OrganizerMaxLength = 100;

            public const string DateFormat = "dd-MM-yyyy";
        }

        public class Accommodation
        {
            public const int AddressMaxLength = 80;

            public const int NotesMaxLength = 80;
        }

        public class Category
        {
            public const int NameMaxLength = 80;
        }

        public class PlayerProfile
        {
            public const int NationalityMaxLength = 40;
            public const int PreferredSurfaceMaxLength = 40;
            public const int DominantHandMaxLength = 15;
        }

        public class AccommodationRequest
        {
            public const int NotesdMaxLength = 350;

        }

    }
}
