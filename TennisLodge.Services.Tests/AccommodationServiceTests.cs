using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class AccommodationServiceTests
    {
        private Mock<IAccommodationRepository> mockAccommodationRepository;
        private AccommodationService accommodationService;

        [SetUp]
        public void Setup()
        {
            this.mockAccommodationRepository = new Mock<IAccommodationRepository>();
            
            this.accommodationService = new AccommodationService(mockAccommodationRepository.Object);
        }

        [Test]
        public async Task AddAccommodationAsync_WithValidModel_ShouldReturnTrue()
        {
            // Arrange
            string userId = "test-user-id";
            AccommodationCreateInputModel model = new AccommodationCreateInputModel
            {
                City = "Sofia",
                Address = "Test Address",
                MaxGuests = 2,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(7),
                Notes = "Test notes"
            };

            this.mockAccommodationRepository
                .Setup(x => x.AddAsync(It.IsAny<Accommodation>()))
                .Returns(Task.CompletedTask);

            // Act
            bool result = await accommodationService.AddAccommodationAsync(userId, model);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.AddAsync(It.Is<Accommodation>(a => 
                a.City == model.City && 
                a.Address == model.Address && 
                a.MaxGuests == model.MaxGuests &&
                a.HostUserId == userId &&
                a.IsAvailable == true)), Times.Once);
        }

        [Test]
        public async Task GetAllAccommodationsAsync_ShouldReturnAvailableAccommodations()
        {
            // Arrange
            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_GetAllAccommodations")
                .Options;

            using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
            AccommodationRepository accommodationRepository = new AccommodationRepository(dbContext);
            AccommodationService accommodationService = new AccommodationService(accommodationRepository);

            List<Accommodation> accommodations = new List<Accommodation>
            {
                new Accommodation
                {
                    Id = 1,
                    City = "Sofia",
                    Address = "Test Address 1",
                    MaxGuests = 2,
                    AvailableFrom = DateTime.UtcNow.AddDays(1),
                    AvailableTo = DateTime.UtcNow.AddDays(7),
                    HostUser = new ApplicationUser { FirstName = "John", LastName = "Doe" },
                    HostUserId = "user1",
                    IsAvailable = true,
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 2,
                    City = "Plovdiv",
                    Address = "Test Address 2",
                    MaxGuests = 3,
                    AvailableFrom = DateTime.UtcNow.AddDays(2),
                    AvailableTo = DateTime.UtcNow.AddDays(8),
                    HostUser = new ApplicationUser { FirstName = "Jane", LastName = "Smith" },
                    HostUserId = "user2",
                    IsAvailable = true,
                    IsDeleted = false
                }
            };

            dbContext.Accommodations.AddRange(accommodations);
            await dbContext.SaveChangesAsync();

            // Act
            IEnumerable<AccommodationViewModel> result = await accommodationService.GetAllAccommodationsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            List<AccommodationViewModel> resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].HostFullName, Is.EqualTo("John Doe"));
            Assert.That(resultList[1].HostFullName, Is.EqualTo("Jane Smith"));
        }

        [Test]
        public async Task IsAccommodationAddedFromUserAsync_WithValidIds_ShouldReturnTrue()
        {
            // Arrange
            string accommodationId = "1";
            string userId = "test-user-id";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                HostUserId = userId
            };

            this.mockAccommodationRepository
                .Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Accommodation, bool>>>()))
                .ReturnsAsync(accommodation);

            // Act
            bool result = await accommodationService.IsAccommodationAddedFromUserAsync(accommodationId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsAccommodationAddedFromUserAsync_WithInvalidAccommodationId_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "invalid-id";
            string userId = "test-user-id";

            // Act
            bool result = await accommodationService.IsAccommodationAddedFromUserAsync(accommodationId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsAccommodationAddedFromUserAsync_WithNullIds_ShouldReturnFalse()
        {
            // Arrange
            string? accommodationId = null;
            string? userId = null;

            // Act
            bool result = await accommodationService.IsAccommodationAddedFromUserAsync(accommodationId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetCreateModel_ShouldReturnNewModel()
        {
            // Act
            AccommodationCreateInputModel? result = accommodationService.GetCreateModel();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<AccommodationCreateInputModel>());
        }

        [Test]
        public async Task EditAccommodationAsync_WithValidModel_ShouldReturnTrue()
        {
            // Arrange
            AccommodationCreateInputModel? model = new AccommodationCreateInputModel
            {
                Id = "1",
                City = "Updated Sofia",
                Address = "Updated Address",
                MaxGuests = 3,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(7),
                Notes = "Updated notes"
            };

            Accommodation existingAccommodation = new Accommodation
            {
                Id = 1,
                City = "Old Sofia",
                Address = "Old Address",
                MaxGuests = 2,
                AvailableFrom = DateTime.UtcNow,
                AvailableTo = DateTime.UtcNow.AddDays(5),
                Notes = "Old notes"
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(existingAccommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(true);

            // Act
            bool result = await accommodationService.EditAccommodationAsync(model);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.UpdateAsync(It.Is<Accommodation>(a => 
                a.City == model.City && 
                a.Address == model.Address && 
                a.MaxGuests == model.MaxGuests)), Times.Once);
        }

        [Test]
        public async Task EditAccommodationAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            AccommodationCreateInputModel model = new AccommodationCreateInputModel
            {
                Id = "invalid-id",
                City = "Sofia",
                Address = "Address",
                MaxGuests = 2
            };

            // Act
            bool result = await accommodationService.EditAccommodationAsync(model);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditAccommodationAsync_WithNullId_ShouldReturnFalse()
        {
            // Arrange
            AccommodationCreateInputModel model = new AccommodationCreateInputModel
            {
                Id = null,
                City = "Sofia",
                Address = "Address",
                MaxGuests = 2
            };

            // Act
            bool result = await accommodationService.EditAccommodationAsync(model);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditAccommodationAsync_WithNonExistentAccommodation_ShouldReturnFalse()
        {
            // Arrange
            AccommodationCreateInputModel model = new AccommodationCreateInputModel
            {
                Id = "999",
                City = "Sofia",
                Address = "Address",
                MaxGuests = 2
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            bool result = await accommodationService.EditAccommodationAsync(model);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetEditableAccommodationByIdAsync_WithValidId_ShouldReturnModel()
        {
            // Arrange
            string accommodationId = "1";
            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_GetEditableAccommodation")
                .Options;

            using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
            AccommodationRepository accommodationRepository = new AccommodationRepository(dbContext);
            AccommodationService accommodationService = new AccommodationService(accommodationRepository);

            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                Address = "Test Address",
                MaxGuests = 2,
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(7),
                Notes = "Test notes",
                HostUserId = "test-user-id"
            };

            dbContext.Accommodations.Add(accommodation);
            await dbContext.SaveChangesAsync();

            // Act
            AccommodationCreateInputModel? result = await accommodationService.GetEditableAccommodationByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo("1"));
            Assert.That(result.City, Is.EqualTo("Sofia"));
            Assert.That(result.Address, Is.EqualTo("Test Address"));
            Assert.That(result.MaxGuests, Is.EqualTo(2));
        }

        [Test]
        public async Task GetEditableAccommodationByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            AccommodationCreateInputModel? result = await accommodationService.GetEditableAccommodationByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetEditableAccommodationByIdAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            string accommodationId = "999";
            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_GetEditableAccommodationNonExistent")
                .Options;

            using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
            AccommodationRepository accommodationRepository = new AccommodationRepository(dbContext);
            AccommodationService accommodationService = new AccommodationService(accommodationRepository);

            // Act
            AccommodationCreateInputModel? result = await accommodationService.GetEditableAccommodationByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SoftDeleteAccommodationAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsDeleted = false
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.DeleteAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(true);

            // Act
            bool result = await accommodationService.SoftDeleteAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.DeleteAsync(It.IsAny<Accommodation>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteAccommodationAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            bool result = await accommodationService.SoftDeleteAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SoftDeleteAccommodationAsync_WithNonExistentAccommodation_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "999";

            mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            bool result = await accommodationService.SoftDeleteAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAccomodationDeleteDetailsByIdAsync_WithValidId_ShouldReturnViewModel()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                Address = "Test Address",
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(7),
                HostUser = new ApplicationUser { UserName = "testuser" },
                HostUserId = "user1"
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            // Act
            AccommodationViewModel? result = await accommodationService.GetAccomodationDeleteDetailsByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.City, Is.EqualTo("Sofia"));
            Assert.That(result.Address, Is.EqualTo("Test Address"));
            Assert.That(result.HostFullName, Is.EqualTo("testuser"));
            Assert.That(result.HostUserId, Is.EqualTo("user1"));
        }

        [Test]
        public async Task GetAccomodationDeleteDetailsByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            AccommodationViewModel? result = await accommodationService.GetAccomodationDeleteDetailsByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAccomodationDeleteDetailsByIdAsync_WithNonExistentAccommodation_ShouldReturnNull()
        {
            // Arrange
            string accommodationId = "999";

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            AccommodationViewModel? result = await accommodationService.GetAccomodationDeleteDetailsByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetAccomodationDeleteDetailsByIdAsync_WithNullHostUser_ShouldReturnUnknownHostName()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                Address = "Test Address",
                AvailableFrom = DateTime.UtcNow.AddDays(1),
                AvailableTo = DateTime.UtcNow.AddDays(7),
                HostUser = null,
                HostUserId = "user1"
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            // Act
            AccommodationViewModel? result = await accommodationService.GetAccomodationDeleteDetailsByIdAsync(accommodationId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.HostFullName, Is.EqualTo("Unknown"));
        }

        [Test]
        public async Task GetAllAccommodationsAsync_ShouldFilterOutDeletedAndUnavailableAccommodations()
        {
            // Arrange
            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_FilterAccommodations")
                .Options;

            using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
            AccommodationRepository accommodationRepository = new AccommodationRepository(dbContext);
            AccommodationService accommodationService = new AccommodationService(accommodationRepository);

            List<Accommodation> accommodations = new List<Accommodation>
            {
                new Accommodation
                {
                    Id = 1,
                    City = "Sofia",
                    Address = "Test Address 1",
                    MaxGuests = 2,
                    AvailableFrom = DateTime.UtcNow.AddDays(1),
                    AvailableTo = DateTime.UtcNow.AddDays(7),
                    HostUser = new ApplicationUser { FirstName = "John", LastName = "Doe" },
                    HostUserId = "user1",
                    IsAvailable = true,
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 2,
                    City = "Plovdiv",
                    Address = "Test Address 2",
                    MaxGuests = 3,
                    AvailableFrom = DateTime.UtcNow.AddDays(2),
                    AvailableTo = DateTime.UtcNow.AddDays(8),
                    HostUser = new ApplicationUser { FirstName = "Jane", LastName = "Smith" },
                    HostUserId = "user2",
                    IsAvailable = false, // Should be filtered out
                    IsDeleted = false
                },
                new Accommodation
                {
                    Id = 3,
                    City = "Varna",
                    Address = "Test Address 3",
                    MaxGuests = 1,
                    AvailableFrom = DateTime.UtcNow.AddDays(3),
                    AvailableTo = DateTime.UtcNow.AddDays(9),
                    HostUser = new ApplicationUser { FirstName = "Bob", LastName = "Johnson" },
                    HostUserId = "user3",
                    IsAvailable = true,
                    IsDeleted = true // Should be filtered out
                }
            };

            dbContext.Accommodations.AddRange(accommodations);
            await dbContext.SaveChangesAsync();

            // Act
            IEnumerable<AccommodationViewModel> result = await accommodationService.GetAllAccommodationsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            List<AccommodationViewModel> resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0].HostFullName, Is.EqualTo("John Doe"));
        }
    }
}
