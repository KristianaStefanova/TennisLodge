using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Web.ViewModels.Favorite;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class FavoriteServiceTests
    {
        private Mock<IFavoriteRepository> mockFavoriteRepository;
        private TennisLodgeDbContext dbContext;
        private FavoriteService favoriteService;

        [SetUp]
        public void Setup()
        {
            mockFavoriteRepository = new Mock<IFavoriteRepository>();

            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            dbContext = new TennisLodgeDbContext(options);
            
            favoriteService = new FavoriteService(mockFavoriteRepository.Object, dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext?.Dispose();
        }


        [TestCase(null, "user-id")]
        [TestCase("tournament-id", null)]
        [TestCase(null, null)]
        [TestCase("invalid-guid", "user-id")]
        public async Task AddTournamentToFavoriteAsync_WithInvalidParameters_ReturnsFalse(string? tournamentId, string? userId)
        {

            // Act
            var result = await favoriteService.AddTournamentToFavoriteAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveTournamentFromFavoriteAsync_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            string tournamentId = Guid.NewGuid().ToString();
            string userId = "test-user-id";
            var existingEntry = new UserTournament
            {
                TournamentId = Guid.Parse(tournamentId),
                UserId = userId
            };

            mockFavoriteRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserTournament, bool>>>()))
                .ReturnsAsync(existingEntry);
            mockFavoriteRepository.Setup(x => x.DeleteAsync(It.IsAny<UserTournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await favoriteService.RemoveTournamentFromFavoriteAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.True);
            mockFavoriteRepository.Verify(x => x.DeleteAsync(It.IsAny<UserTournament>()), Times.Once);
        }

        [Test]
        public async Task RemoveTournamentFromFavoriteAsync_WithNonExistentEntry_ReturnsFalse()
        {
            // Arrange
            string tournamentId = Guid.NewGuid().ToString();
            string userId = "test-user-id";

            mockFavoriteRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserTournament, bool>>>()))
                .ReturnsAsync((UserTournament)null);

            // Act
            bool result = await favoriteService.RemoveTournamentFromFavoriteAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.False);
            mockFavoriteRepository.Verify(x => x.DeleteAsync(It.IsAny<UserTournament>()), Times.Never);
        }

        [TestCase(null, "user-id")]
        [TestCase("tournament-id", null)]
        [TestCase(null, null)]
        [TestCase("invalid-guid", "user-id")]
        public async Task RemoveTournamentFromFavoriteAsync_WithInvalidParameters_ReturnsFalse(string? tournamentId, string? userId)
        {
            // Act
            bool result = await favoriteService.RemoveTournamentFromFavoriteAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsTournamentInFavoritesAsync_WithExistingEntry_ReturnsTrue()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            string userId = "test-user-id";
            UserTournament existingEntry = new UserTournament
            {
                TournamentId = tournamentId,
                UserId = userId
            };

            mockFavoriteRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserTournament, bool>>>()))
                .ReturnsAsync(existingEntry);

            // Act
            bool result = await favoriteService.IsTournamentInFavoritesAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsTournamentInFavoritesAsync_WithNonExistentEntry_ReturnsFalse()
        {
            // Arrange
            Guid tournamentId = Guid.NewGuid();
            string userId = "test-user-id";

            mockFavoriteRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserTournament, bool>>>()))
                .ReturnsAsync((UserTournament)null);

            // Act
            bool result = await favoriteService.IsTournamentInFavoritesAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [TestCase(null, "user-id")]
        [TestCase(null, null)]
        [TestCase(null, "")]
        public async Task IsTournamentInFavoritesAsync_WithInvalidParameters_ReturnsFalse(Guid? tournamentId, string? userId)
        {
            
            // Act
            bool result = await favoriteService.IsTournamentInFavoritesAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        

        [Test]
        public async Task RemoveTournamentFromFavoriteAsync_WithValidGuid_ReturnsTrue()
        {
            // Arrange
            string tournamentId = "12345678-1234-1234-1234-123456789012";
            string userId = "test-user-id";
            UserTournament existingEntry = new UserTournament
            {
                TournamentId = Guid.Parse(tournamentId),
                UserId = userId
            };

            mockFavoriteRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<UserTournament, bool>>>()))
                .ReturnsAsync(existingEntry);
            mockFavoriteRepository.Setup(x => x.DeleteAsync(It.IsAny<UserTournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await favoriteService.RemoveTournamentFromFavoriteAsync(tournamentId, userId);

            // Assert
            Assert.That(result, Is.True);
            mockFavoriteRepository.Verify(x => x.DeleteAsync(It.IsAny<UserTournament>()), Times.Once);
        }

        private IQueryable<UserTournament> CreateMockUserTournamentQueryable(string userId)
        {
            var tournaments = new List<UserTournament>
            {
                new UserTournament
                {
                    UserId = userId,
                    TournamentId = Guid.NewGuid(),
                    Tournament = new Tournament
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tournament 1",
                        Location = "Location 1",
                        Category = new Category { Name = "Category 1" },
                        StartDate = DateOnly.FromDateTime(DateTime.Now),
                        EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                        ImageUrl = "image1.jpg"
                    }
                },
                new UserTournament
                {
                    UserId = userId,
                    TournamentId = Guid.NewGuid(),
                    Tournament = new Tournament
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tournament 2",
                        Location = "Location 2",
                        Category = new Category { Name = "Category 2" },
                        StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                        EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(8)),
                        ImageUrl = null
                    }
                }
            };

            return tournaments.AsQueryable();
        }
    }
}
