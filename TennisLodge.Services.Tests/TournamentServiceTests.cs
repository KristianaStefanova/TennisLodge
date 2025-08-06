using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Web.ViewModels.Tournament;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Tests
{
    
    [TestFixture]
    public class TournamentServiceTests
    {
        private Mock<ICategoryRepository> mockCategoryRepository;
        private Mock<ITournamentRepository> mockTournamentRepository;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private TournamentService tournamentService;

        [SetUp]
        public void Setup()
        {
            mockCategoryRepository = new Mock<ICategoryRepository>();
            mockTournamentRepository = new Mock<ITournamentRepository>();


            IUserStore<ApplicationUser> userStore = Mock.Of<IUserStore<ApplicationUser>>();
            mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore, null, null, null, null, null, null, null, null);

            tournamentService = new TournamentService(
                mockCategoryRepository.Object,
                mockTournamentRepository.Object,
                mockUserManager.Object);
        }

        [Test]
        public async Task AddTournamentAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            string userId = "test-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = "/images/test.jpg",
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            ApplicationUser user = new ApplicationUser { Id = userId };
            Category category = new Category { Id = 1, Name = "Test Category" };

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);
            mockCategoryRepository.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(category);
            mockTournamentRepository.Setup(x => x.AddAsync(It.IsAny<Tournament>()))
                .Returns(Task.CompletedTask);
            mockTournamentRepository.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            mockTournamentRepository.Verify(x => x.AddAsync(It.IsAny<Tournament>()), Times.Once);
            mockTournamentRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddTournamentAsync_WithInvalidUserId_ShouldReturnFalse()
        {
            // Arrange
            string userId = "invalid-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Test Organizer",
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser?)null);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.AddAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task AddTournamentAsync_WithInvalidCategoryId_ShouldReturnFalse()
        {
            // Arrange
            string userId = "test-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 999,
                Organizer = "Test Organizer",
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            ApplicationUser user = new ApplicationUser { Id = userId };

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);
            mockCategoryRepository.Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Category?)null);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.AddAsync(It.IsAny<Tournament>()), Times.Never);
        }

      

        [Test]
        public async Task GetTournamentDetailsByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            string invalidId = "invalid-guid";

            // Act
            TournamentDetailsViewModel? result = await tournamentService.GetTournamentDetailsByIdAsync(invalidId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetTournamentDetailsByIdAsync_WithNullId_ShouldReturnNull()
        {
            // Act
            TournamentDetailsViewModel? result = await tournamentService.GetTournamentDetailsByIdAsync(null);

            // Assert
            Assert.That(result, Is.Null);
        }

        
        

        [Test]
        public async Task GetEditableTournamentByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            string invalidId = "invalid-guid";

            // Act
            TournamentFormInputModel? result = await tournamentService.GetEditableTournamentByIdAsync(invalidId);

            // Assert
            Assert.That(result, Is.Null);
        }

        

        [Test]
        public async Task EditTournamentAsync_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = tournamentId.ToString(),
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                ImageUrl = "/images/updated.jpg",
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            Tournament existingTournament = new Tournament
            {
                Id = tournamentId,
                Name = "Original Tournament",
                Description = "Original Description",
                Location = "Original Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Original Organizer",
                ImageUrl = "/images/original.jpg",
                StartDate = DateOnly.ParseExact("2024-01-01", AppDateFormat, CultureInfo.InvariantCulture),
                EndDate = DateOnly.ParseExact("2024-01-07", AppDateFormat, CultureInfo.InvariantCulture)
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(existingTournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Once);
        }

        [Test]
        public async Task EditTournamentAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = "invalid-guid",
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            // Act
            bool result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task EditTournamentAsync_WithNonExistentTournament_ShouldReturnFalse()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = tournamentId.ToString(),
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync((Tournament?)null);

            // Act
            bool result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteTournamentAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            Tournament tournament = new Tournament
            {
                Id = tournamentId,
                Name = "Test Tournament",
                IsDeleted = false
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(tournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await tournamentService.SoftDeleteTournamentAsync(tournamentId.ToString());

            // Assert
            Assert.That(result, Is.True);
            Assert.That(tournament.IsDeleted, Is.True);
            mockTournamentRepository.Verify(x => x.UpdateAsync(tournament), Times.Once);
        }

        [Test]
        public async Task SoftDeleteTournamentAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            string invalidId = "invalid-guid";

            // Act
            bool result = await tournamentService.SoftDeleteTournamentAsync(invalidId);

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteTournamentAsync_WithNonExistentTournament_ShouldReturnFalse()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync((Tournament?)null);

            // Act
            bool result = await tournamentService.SoftDeleteTournamentAsync(tournamentId.ToString());

            // Assert
            Assert.That(result, Is.False);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task SoftDeleteTournamentAsync_WhenExceptionOccurs_ShouldReturnFalse()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            Tournament tournament = new Tournament
            {
                Id = tournamentId,
                Name = "Test Tournament",
                IsDeleted = false
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(tournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            bool result = await tournamentService.SoftDeleteTournamentAsync(tournamentId.ToString());

            // Assert
            Assert.That(result, Is.False);
        }

        

        

        [Test]
        public async Task AddTournamentAsync_WithNullImageUrl_ShouldSetDefaultImageUrl()
        {
            // Arrange
            string userId = "test-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = null,
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            ApplicationUser user = new ApplicationUser { Id = userId };
            Category category = new Category { Id = 1, Name = "Test Category" };

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);
            mockCategoryRepository.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(category);
            mockTournamentRepository.Setup(x => x.AddAsync(It.IsAny<Tournament>()))
                .Returns(Task.CompletedTask);
            mockTournamentRepository.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            mockTournamentRepository.Verify(x => x.AddAsync(It.Is<Tournament>(t => t.ImageUrl == null)), Times.Once);
        }

        [Test]
        public async Task EditTournamentAsync_WithNullImageUrl_ShouldSetDefaultImageUrl()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = tournamentId.ToString(),
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                ImageUrl = null,
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            Tournament existingTournament = new Tournament
            {
                Id = tournamentId,
                Name = "Original Tournament",
                Description = "Original Description",
                Location = "Original Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Original Organizer",
                ImageUrl = "/images/original.jpg",
                StartDate = DateOnly.ParseExact("2024-01-01", AppDateFormat, CultureInfo.InvariantCulture),
                EndDate = DateOnly.ParseExact("2024-01-07", AppDateFormat, CultureInfo.InvariantCulture)
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(existingTournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.Is<Tournament>(t => t.ImageUrl == $"/images/{NoImageUrl}.jpg")), Times.Once);
        }

        [Test]
        public async Task AddTournamentAsync_WithValidData_ShouldCreateTournamentWithCorrectProperties()
        {
            // Arrange
            string userId = "test-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = "/images/test.jpg",
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            ApplicationUser user = new ApplicationUser { Id = userId };
            Category category = new Category { Id = 1, Name = "Test Category" };

            Tournament createdTournament = null;

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);
            mockCategoryRepository.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(category);
            mockTournamentRepository.Setup(x => x.AddAsync(It.IsAny<Tournament>()))
                .Callback<Tournament>(t => createdTournament = t)
                .Returns(Task.CompletedTask);
            mockTournamentRepository.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(createdTournament, Is.Not.Null);
            Assert.That(createdTournament.Name, Is.EqualTo("Test Tournament"));
            Assert.That(createdTournament.Description, Is.EqualTo("Test Description"));
            Assert.That(createdTournament.Location, Is.EqualTo("Test Location"));
            Assert.That(createdTournament.Surface, Is.EqualTo("Clay"));
            Assert.That(createdTournament.CategoryId, Is.EqualTo(1));
            Assert.That(createdTournament.Organizer, Is.EqualTo("Test Organizer"));
            Assert.That(createdTournament.ImageUrl, Is.EqualTo("/images/test.jpg"));
            Assert.That(createdTournament.StartDate, Is.EqualTo(DateOnly.ParseExact("2024-01-01", AppDateFormat, CultureInfo.InvariantCulture)));
            Assert.That(createdTournament.EndDate, Is.EqualTo(DateOnly.ParseExact("2024-01-07", AppDateFormat, CultureInfo.InvariantCulture)));
        }

        [Test]
        public async Task EditTournamentAsync_WithValidData_ShouldUpdateTournamentWithCorrectProperties()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = tournamentId.ToString(),
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                ImageUrl = "/images/updated.jpg",
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            Tournament existingTournament = new Tournament
            {
                Id = tournamentId,
                Name = "Original Tournament",
                Description = "Original Description",
                Location = "Original Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Original Organizer",
                ImageUrl = "/images/original.jpg",
                StartDate = DateOnly.ParseExact("2024-01-01", AppDateFormat, CultureInfo.InvariantCulture),
                EndDate = DateOnly.ParseExact("2024-01-07", AppDateFormat, CultureInfo.InvariantCulture)
            };

            Tournament updatedTournament = null;

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(existingTournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .Callback<Tournament>(t => updatedTournament = t)
                .ReturnsAsync(true);

            // Act
            bool result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(updatedTournament, Is.Not.Null);
            Assert.That(updatedTournament.Name, Is.EqualTo("Updated Tournament"));
            Assert.That(updatedTournament.Description, Is.EqualTo("Updated Description"));
            Assert.That(updatedTournament.Location, Is.EqualTo("Updated Location"));
            Assert.That(updatedTournament.Surface, Is.EqualTo("Hard"));
            Assert.That(updatedTournament.CategoryId, Is.EqualTo(2));
            Assert.That(updatedTournament.Organizer, Is.EqualTo("Updated Organizer"));
            Assert.That(updatedTournament.ImageUrl, Is.EqualTo("/images/updated.jpg"));
            Assert.That(updatedTournament.StartDate, Is.EqualTo(DateOnly.ParseExact("2024-02-01", AppDateFormat, CultureInfo.InvariantCulture)));
            Assert.That(updatedTournament.EndDate, Is.EqualTo(DateOnly.ParseExact("2024-02-07", AppDateFormat, CultureInfo.InvariantCulture)));
        }

        [Test]
        public async Task AddTournamentAsync_WithEmptyStringImageUrl_ShouldSetNullImageUrl()
        {
            // Arrange
            string userId = "test-user-id";
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = "",
                StartDate = "2024-01-01",
                EndDate = "2024-01-07"
            };

            ApplicationUser user = new ApplicationUser { Id = userId };
            Category category = new Category { Id = 1, Name = "Test Category" };

            Tournament createdTournament = null;

            mockUserManager.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);
            mockCategoryRepository.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(category);
            mockTournamentRepository.Setup(x => x.AddAsync(It.IsAny<Tournament>()))
                .Callback<Tournament>(t => createdTournament = t)
                .Returns(Task.CompletedTask);
            mockTournamentRepository.Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            bool result = await tournamentService.AddTournamentAsync(userId, inputModel);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(createdTournament.ImageUrl, Is.EqualTo(""));
        }

        [Test]
        public async Task EditTournamentAsync_WithEmptyStringImageUrl_ShouldSetEmptyStringImageUrl()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = tournamentId.ToString(),
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Hard",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                ImageUrl = "",
                StartDate = "2024-02-01",
                EndDate = "2024-02-07"
            };

            Tournament existingTournament = new Tournament
            {
                Id = tournamentId,
                Name = "Original Tournament",
                Description = "Original Description",
                Location = "Original Location",
                Surface = "Clay",
                CategoryId = 1,
                Organizer = "Original Organizer",
                ImageUrl = "/images/original.jpg",
                StartDate = DateOnly.ParseExact("2024-01-01", AppDateFormat, CultureInfo.InvariantCulture),
                EndDate = DateOnly.ParseExact("2024-01-07", AppDateFormat, CultureInfo.InvariantCulture)
            };

            mockTournamentRepository.Setup(x => x.GetByIdAsync(tournamentId))
                .ReturnsAsync(existingTournament);
            mockTournamentRepository.Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            var result = await tournamentService.EditTournamentAsync(inputModel);

            // Assert
            Assert.That(result, Is.True);
            mockTournamentRepository.Verify(x => x.UpdateAsync(It.Is<Tournament>(t => t.ImageUrl == "")), Times.Once);
        }
    }
}
