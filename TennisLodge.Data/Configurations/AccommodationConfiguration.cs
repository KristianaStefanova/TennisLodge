using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisLodge.Data.Models;
using static TennisLodge.Data.Common.EntityConstants.Accommodation;

namespace TennisLodge.Data.Configurations
{
    public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {

            builder
                .HasKey(a => a.Id);


            builder
                .Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Primary key for the Accommodation entity");


            builder
                .Property(a => a.HostUserId)
                .IsRequired()
                .HasComment("Foreign key to the user offering the accommodation");


            builder
                .HasOne(a => a.HostUser)
                .WithMany(u => u.AccommodationsOffered)
                .HasForeignKey(a => a.HostUserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(CityMaxLength)
                .HasComment("City where the accommodation is located");


            builder
                .Property(a => a.Address)
                .IsRequired()
                .HasMaxLength(AddressMaxLength);


            builder
                .Property(a => a.MaxGuests)
                .IsRequired()
                .HasComment("Maximum number of guests that can be hosted");


            builder
                .Property(a => a.IsAvailable)
                .HasDefaultValue(true)
                .HasComment("Indicates if the accommodation is currently available");


            builder
                .Property(a => a.Notes)
                .HasMaxLength(NotesMaxLength)
                .HasComment("Optional description or notes about the accommodation");


            builder
                .Property(a => a.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                 .Property(a => a.AvailableFrom)
                 .IsRequired()
                 .HasComment("Start of availability window");


            builder
                .Property(a => a.AvailableTo)
                .IsRequired()
                .HasComment("End of availability window");

            builder
                .HasData(GenerateSeedAccommodations());
        }

        private List<Accommodation> GenerateSeedAccommodations()
        {
            List<Accommodation> seedAccommodations = new List<Accommodation>()
            {
                new Accommodation
                {
                    Id = 1,
                    HostUserId = "31b0fae9-597d-46ce-95be-8c6e51cccf92",
                    City = "Sofia",
                    Address = "булевард Цариградско шосе 101, Sofia",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Подходящо за млади спортисти с придружители.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 2,
                    HostUserId = "31b0fae9-597d-46ce-95be-8c6e51cccf92",
                    City = "Plovdiv",
                    Address = "ул. Васил Левски 15, Plovdiv",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Самостоятелна стая с баня, идеална за по-големи спортисти.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2026, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 3,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Varna",
                    Address = "булевард Княгиня Мария Луиза 45, Varna",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Намира се близо до спортен комплекс, с Wi-Fi и кухня.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2026, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 4,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Burgas",
                    Address = "ул. Христо Ботев 22, Burgas",
                    MaxGuests = 4,
                    IsAvailable = true,
                    Notes = "Идеално за семейства и треньори, близо до плажа.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 5,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Ruse",
                    Address = "ул. Съборна 7, Ruse",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Удобно за индивидуални спортисти.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 12, 7, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 6,
                    HostUserId = "556cff19-6dd1-4005-836d-420f8b3877f8",
                    City = "Sofia",
                    Address = "ул. Капитан Райчо 12, Sofia",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Близо до метро и спортен център.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 7,
                    HostUserId = "556cff19-6dd1-4005-836d-420f8b3877f8",
                    City = "Plovdiv",
                    Address = "булевард Руски 78, Plovdiv",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Кухня и паркинг на разположение.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 7, 27, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 7, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 8,
                    HostUserId = "31b0fae9-597d-46ce-95be-8c6e51cccf92",
                    City = "Varna",
                    Address = "ул. Гладстон 30, Varna",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Спокоен район, без домашни любимци.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 9, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 9,
                    HostUserId = "31b0fae9-597d-46ce-95be-8c6e51cccf92",
                    City = "Burgas",
                    Address = "ул. Иван Вазов 44, Burgas",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Близо до плажа и спортни съоръжения.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 9, 5, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 10,
                    HostUserId = "072d4f63-2658-489b-a874-1e32d50b8837",
                    City = "Ruse",
                    Address = "ул. Гео Милев 55, Ruse",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Идеално за индивидуални състезатели.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 7, 25, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 10, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 11,
                    HostUserId = "072d4f63-2658-489b-a874-1e32d50b8837",
                    City = "Stara Zagora",
                    Address = "ул. Христо Ботев 22, Stara Zagora",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Идеално за семейства с малки деца.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 12,
                    HostUserId = "716bc195-efc6-4637-943a-a3a80f086e65",
                    City = "Pleven",
                    Address = "ул. Васил Левски 15, Pleven",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Тих район, удобен за тренировки.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 9, 20, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 13,
                    HostUserId = "716bc195-efc6-4637-943a-a3a80f086e65",
                    City = "Sliven",
                    Address = "булевард Княгиня Мария Луиза 45, Sliven",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Подходящо за самостоятелни спортисти.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 9, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 14,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Dobrich",
                    Address = "ул. Съборна 7, Dobrich",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Близо до спортна зала и фитнес.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 8, 15, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 15,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Blagoevgrad",
                    Address = "ул. Васил Кънчов 12, Blagoevgrad",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Уютен апартамент близо до центъра.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 16,
                    HostUserId = "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902",
                    City = "Veliko Tarnovo",
                    Address = "бул. Цар Освободител 34, Veliko Tarnovo",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Просторен дом с прекрасна гледка.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 11, 18, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 17,
                    HostUserId = "ede3325f-5350-496e-b5d5-339823b8d5b1",
                    City = "Gabrovo",
                    Address = "ул. Христо Смирненски 7, Gabrovo",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Тихо място, подходящо за индивидуални спортисти.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 18,
                    HostUserId = "ede3325f-5350-496e-b5d5-339823b8d5b1",
                    City = "Pazardzhik",
                    Address = "ул. Гео Милев 19, Pazardzhik",
                    MaxGuests = 4,
                    IsAvailable = false,
                    Notes = "Голяма къща с двор, подходяща за групи.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 10, 29, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 19,
                    HostUserId = "9dbe7540-f8cd-43c7-b2df-f3947135d8bf",
                    City = "Haskovo",
                    Address = "ул. Ст. Караджа 10, Haskovo",
                    MaxGuests = 2,
                    IsAvailable = true,
                    Notes = "Добре обзаведен апартамент, близо до спортна зала.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 10, 10, 0, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 10, 28, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 20,
                    HostUserId = "9dbe7540-f8cd-43c7-b2df-f3947135d8bf",
                    City = "Dobrich",
                    Address = "ул. Ген. Скобелев 25, Dobrich",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Самостоятелна стая с достъп до кухня.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 9, 1, 13, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 9, 10, 22, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 21,
                    HostUserId = "763aebd4-e72a-41d7-bd9e-7ecbc0dba1a8",
                    City = "Kazanlak",
                    Address = "ул. Розова Долина 8, Kazanlak",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Тих квартал, удобен за тренировки и отдих.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 10, 20, 13, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 10, 28, 22, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 22,
                    HostUserId = "763aebd4-e72a-41d7-bd9e-7ecbc0dba1a8",
                    City = "Shumen",
                    Address = "ул. Съборна 3, Shumen",
                    MaxGuests = 2,
                    IsAvailable = false,
                    Notes = "Добре оборудвана къща близо до центъра.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 8, 25, 18, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 30, 15, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 23,
                    HostUserId = "004c1a89-ede7-471f-ac81-93c6a9d8b0d8",
                    City = "Targovishte",
                    Address = "бул. България 17, Targovishte",
                    MaxGuests = 1,
                    IsAvailable = true,
                    Notes = "Самостоятелна стая с добър достъп до транспорт.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2026, 1, 20, 13, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2026, 10, 28, 15, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 24,
                    HostUserId = "004c1a89-ede7-471f-ac81-93c6a9d8b0d8",
                    City = "Montana",
                    Address = "ул. Пирин 14, Montana",
                    MaxGuests = 3,
                    IsAvailable = true,
                    Notes = "Просторен апартамент, подходящ за групи.",
                    CreatedOn = DateTime.UtcNow,
                    AvailableFrom = new DateTime(2025, 8, 5, 12, 0, 0, DateTimeKind.Utc),
                    AvailableTo = new DateTime(2025, 8, 25, 12, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            };

            return seedAccommodations;
        }
    }
}

