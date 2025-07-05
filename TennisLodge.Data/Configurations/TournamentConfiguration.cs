using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;
using static TennisLodge.Data.Common.EntityConstants.Tournament;

namespace TennisLodge.Data.Models.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> entity)
        {
            entity
                .HasKey(t => t.Id);


            entity
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);


            entity
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);


            entity
                .Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(LocationMaxLength);


            entity
                .Property(t => t.Surface)
                .IsRequired()
                .HasMaxLength(SurfaceMaxLength);


            entity
                .Property(t => t.Organizer)
                .IsRequired()
                .HasMaxLength(OrganizerMaxLength);


            entity
                .Property(t => t.ImageUrl)
                .IsRequired(false);


            entity
                .Property(t => t.StartDate)
                .IsRequired();


            entity
                .Property(t => t.EndDate)
                .IsRequired();


            entity
                .Property(t => t.IsDeleted)
                .HasDefaultValue(false);


            entity
                .Property(t => t.PublisherId)
                .IsRequired();


            entity
               .HasQueryFilter(d => d.IsDeleted == false);


            entity
                .HasOne(t => t.Category)
                .WithMany(t => t.Tournaments)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            entity
                .HasOne(t => t.Publisher)
                .WithMany(u => u.PublishedTournaments)
                .HasForeignKey(t => t.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);


            entity
                .HasData(this.GenerateSeedTournaments());
        }

        private List<Tournament> GenerateSeedTournaments()
        {
            List<Tournament> seedTournaments = new List<Tournament>()
    {
        new Tournament
        {
            Id = Guid.Parse("8f19c979-40c2-4cb8-8af0-d061456245bd"),
            Name = "Sofia Open",
            Description = "Professional indoor hard court tournament held annually in Sofia, Bulgaria.",
            Location = "Sofia, Arena Armeec",
            Surface = "Hard (Indoor)",
            ImageUrl = "/images/atp250.jpg",
            Organizer = "Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 10, 1),
            EndDate = new DateOnly(2025, 10, 7),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 1, 
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("7b6d0baf-2a39-41d7-bc2a-e5713b302baf"),
            Name = "Plovdiv Clay Cup",
            Description = "Challenger-level clay court tournament attracting top Eastern European players.",
            Location = "Plovdiv, Tennis Complex",
            Surface = "Clay",
            ImageUrl = "/images/ChallengerTour.jpg",
            Organizer = "Plovdiv Tennis Club",
            StartDate = new DateOnly(2025, 6, 10),
            EndDate = new DateOnly(2025, 6, 16),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 2, // Challenger
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("ea09c21c-62f5-4ee2-99bb-d63f682c5ee3"),
            Name = "Burgas U10 Open",
            Description = "National tournament for kids under 10, designed to encourage early development.",
            Location = "Burgas Tennis Club",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Burgas Youth Sports",
            StartDate = new DateOnly(2025, 5, 2),
            EndDate = new DateOnly(2025, 5, 4),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 4, // Under 10
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("e260f1e1-b12b-4e2b-87c8-e2df1f388377"),
            Name = "Stara Zagora U12 Cup",
            Description = "National ranking tournament for players under 12 years old.",
            Location = "Stara Zagora Tennis Arena",
            Surface = "Hard",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 4, 18),
            EndDate = new DateOnly(2025, 4, 21),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 5, // Under 12
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("a5f6700a-3e08-430b-9462-2b2f61d31af2"),
            Name = "Blagoevgrad U14 Masters",
            Description = "Elite tournament for U14 players across the country.",
            Location = "Blagoevgrad Tennis Club",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "National Youth Tennis",
            StartDate = new DateOnly(2025, 7, 10),
            EndDate = new DateOnly(2025, 7, 14),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 6, // Under 14
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("fe9804cb-0ae2-42b6-a1fd-d90c0bc880ec"),
            Name = "National Youth Championship",
            Description = "Annual national youth championship open to all categories.",
            Location = "Sofia National Center",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 9, 1),
            EndDate = new DateOnly(2025, 9, 7),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 7, 
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("0ceea4f9-b7bc-4d18-90cf-3b64b06c2a1f"),
            Name = "Tennis Europe U14 – Albena",
            Description = "Tennis Europe international event held in Albena for top U14 talents.",
            Location = "Albena Resort Courts",
            Surface = "Hard",
            ImageUrl = "/images/TennisEurope.jpg",
            Organizer = "Tennis Europe / BTF",
            StartDate = new DateOnly(2025, 8, 12),
            EndDate = new DateOnly(2025, 8, 18),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 8, // Tennis Europe
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("c909f34e-8126-42c5-9e65-8d9c0ff0db96"),
            Name = "ITF Juniors - Sofia",
            Description = "International ITF Junior tournament held in Bulgaria's capital.",
            Location = "Sofia, National Tennis Center",
            Surface = "Hard",
            ImageUrl = "/images/ITFJuniors.jpg",
            Organizer = "ITF / Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 11, 1),
            EndDate = new DateOnly(2025, 11, 7),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 9, // ITF Juniors
            IsDeleted = false
        },
        new Tournament
             {
             Id = Guid.Parse("a1f0c8c7-f4b3-4eb9-82e3-4f8cbf2066ee"),
             Name = "ATP 250 Varna",
             Description = "ATP 250 series tournament on the Black Sea coast.",
             Location = "Varna Tennis Club",
             Surface = "Hard (Outdoor)",
             ImageUrl = "/images/atp250.jpg",
             Organizer = "Bulgarian Tennis Federation",
             StartDate = new DateOnly(2025, 10, 15),
             EndDate = new DateOnly(2025, 10, 21),
             PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
             CategoryId = 1,
             IsDeleted = false
             },
        new Tournament
        {
            Id = Guid.Parse("b5d3462f-7f51-4382-b57c-cc1464a68f1f"),
            Name = "Challenger Rousse Open",
            Description = "Challenger Tour event played on indoor hard court.",
            Location = "Rousse Arena",
            Surface = "Hard (Indoor)",
            ImageUrl = "/images/ChallengerTour.jpg",
            Organizer = "Rousse Tennis Association",
            StartDate = new DateOnly(2025, 6, 20),
            EndDate = new DateOnly(2025, 6, 26),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 2,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("dff35c76-7d20-4203-aaa1-7cb5117fd9f7"),
            Name = "Kidz Clay Open - Veliko Tarnovo",
            Description = "Introductory tournament for players under 10.",
            Location = "Veliko Tarnovo Tennis Park",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Youth Tennis Bulgaria",
            StartDate = new DateOnly(2025, 5, 15),
            EndDate = new DateOnly(2025, 5, 17),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 4,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("e0c9381f-f3ff-48cc-8719-30a4c649844a"),
            Name = "U12 Green Cup – Dobrich",
            Description = "Official U12 tournament on clay courts.",
            Location = "Dobrich Tennis Club",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Dobrich Tennis Foundation",
            StartDate = new DateOnly(2025, 4, 25),
            EndDate = new DateOnly(2025, 4, 28),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 5,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("f540e688-b37a-4061-9bc4-0d4b1bdbd1e6"),
            Name = "Samokov U14 Cup",
            Description = "Important regional tournament for U14 talents.",
            Location = "Samokov Tennis Arena",
            Surface = "Hard",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Samokov Tennis League",
            StartDate = new DateOnly(2025, 7, 5),
            EndDate = new DateOnly(2025, 7, 9),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 6,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("90bb0d0a-20ae-4952-804b-bb9e3bcecb18"),
            Name = "National Juniors Teams Cup",
            Description = "Team-based tournament for the best clubs nationwide.",
            Location = "Sofia Tennis Center",
            Surface = "Clay",
            ImageUrl = "/images/BulgarianFederation.jpg",
            Organizer = "Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 9, 15),
            EndDate = new DateOnly(2025, 9, 20),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 7,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("a9f5d6c3-7d5a-4a47-95ed-31080b39a04b"),
            Name = "Tennis Europe U16 – Varna",
            Description = "U16 Tennis Europe event hosted at the Black Sea coast.",
            Location = "Varna Tennis Arena",
            Surface = "Hard",
            ImageUrl = "/images/TennisEurope.jpg",
            Organizer = "Tennis Europe / BTF",
            StartDate = new DateOnly(2025, 8, 25),
            EndDate = new DateOnly(2025, 8, 31),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 8,
            IsDeleted = false
        },
        new Tournament
        {
            Id = Guid.Parse("d27a295f-2b7c-4fc1-9113-89672539e12c"),
            Name = "ITF Juniors - Plovdiv",
            Description = "High-level ITF juniors event held in Plovdiv.",
            Location = "Plovdiv Tennis Arena",
            Surface = "Clay",
            ImageUrl = "/images/ITFJuniors.jpg",
            Organizer = "ITF / Bulgarian Tennis Federation",
            StartDate = new DateOnly(2025, 11, 12),
            EndDate = new DateOnly(2025, 11, 18),
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            CategoryId = 9,
            IsDeleted = false
        }
    };

            return seedTournaments;
        }
    }
}

