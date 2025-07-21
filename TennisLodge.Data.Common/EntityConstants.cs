using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Common
{
    public static class EntityConstants
    {
        public static class Tournament
        {
            /// <summary>
            /// Tournament Name should be at least 2 characters and up to 100 characters.
            /// </summary>
            public const int NameMinLength = 2;

            /// <summary>
            /// Tournament Name should be able to store text with length up to 100 characters.
            /// </summary>
            public const int NameMaxLength = 100;

            /// <summary>
            /// Tournament Location must be at least 3 characters.
            /// </summary>
            public const int LocationMinLength = 3;

            /// <summary>
            /// Tournament Location should be able to store text with length up to 50 characters.
            /// </summary>
            public const int LocationMaxLength = 50;

            /// <summary>
            /// Organizer name must be at least 2 characters.
            /// </summary>
            public const int OrganizerNameMinLength = 2;

            /// <summary>
            /// Tournament Organizer should be able to store text with length up to 50 characters.
            /// </summary>
            public const int OrganizerNameMaxLength = 50;

            /// <summary>
            /// Tournament Description must be at least 10 characters.
            /// </summary>
            public const int DescriptionMinLength = 10;

            /// <summary>
            /// Tournament Description should be able to store text with length up to 1000 characters.
            /// </summary>
            public const int DescriptionMaxLength = 1000;

            /// <summary>
            /// Tournament Surface must be at least 3 characters.
            /// </summary>
            public const int SurfaceMinLength = 3;

            /// <summary>
            /// Tournament Surface should be able to store text with length up to 30 characters.
            /// </summary>
            public const int SurfaceMaxLength = 30;

            /// <summary>
            /// Tournament Category must be at least 3 characters.
            /// </summary>
            public const int CategoryMinLength = 3;

            /// <summary>
            /// Tournament Category should be able to store text with length up to 30 characters.
            /// </summary>
            public const int CategoryMaxLength = 30;

            /// <summary>
            /// Maximum allowed length for image URL.
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Accommodation
        {
            public const int AddressMinLength = 2;
            public const int AddressMaxLength = 80;

            public const int NotesMaxLength = 80;

            public const int MinGuestsCount = 1;
            public const int MaxGuestsCount = 20;

            public const int CityMinLength = 2;
            public const int CityMaxLength = 50;
        }

        public static class Category
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
