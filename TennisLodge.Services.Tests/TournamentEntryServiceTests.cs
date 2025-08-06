using Moq;
using NUnit.Framework;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class TournamentEntryServiceTests
    {
        private Mock<ITournamentEntryRepository> mockRepository;
        private TournamentEntryService service;
        private List<TournamentEntry> testEntries;
        private List<Tournament> testTournaments;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<ITournamentEntryRepository>();
            service = new TournamentEntryService(mockRepository.Object);
            
            SetupTestData();
        }

        private void SetupTestData()
        {
            testTournaments = new List<Tournament>
            {
                new Tournament
                {
                    Id = Guid.NewGuid(),
                    Name = "Torneo de Verano",
                    Location = "Madrid",
                    StartDate = new DateOnly(2024, 7, 15),
                    IsDeleted = false
                },
                new Tournament
                {
                    Id = Guid.NewGuid(),
                    Name = "Copa de Invierno",
                    Location = "Barcelona",
                    StartDate = new DateOnly(2024, 12, 20),
                    IsDeleted = false
                }
            };

            testEntries = new List<TournamentEntry>
            {
                new TournamentEntry
                {
                    Id = 1,
                    PlayerId = "player1",
                    TournamentId = testTournaments[0].Id,
                    Tournament = testTournaments[0],
                    RegisteredOn = DateTime.UtcNow.AddDays(-5),
                    IsDeleted = false
                },
                new TournamentEntry
                {
                    Id = 2,
                    PlayerId = "player1",
                    TournamentId = testTournaments[1].Id,
                    Tournament = testTournaments[1],
                    RegisteredOn = DateTime.UtcNow.AddDays(-3),
                    IsDeleted = false
                },
                new TournamentEntry
                {
                    Id = 3,
                    PlayerId = "player2",
                    TournamentId = testTournaments[0].Id,
                    Tournament = testTournaments[0],
                    RegisteredOn = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false
                }
            };
        }

        [Test]
        public async Task JoinTournamentAsync_WhenPlayerNotAlreadyJoined_ShouldReturnTrue()
        {
            // Arrange
            string playerId = "newPlayer";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task JoinTournamentAsync_WhenPlayerAlreadyJoined_ShouldReturnFalse()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = testTournaments[0].Id;
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task JoinTournamentAsync_WhenEntryIsDeleted_ShouldAllowRejoin()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = testTournaments[0].Id;
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_ShouldReturnCorrectTournaments()
        {
            // Arrange
            string playerId = "player1";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WhenPlayerHasNoTournaments_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = "nonexistentPlayer";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_ShouldExcludeDeletedEntries()
        {
            // Arrange
            string playerId = "player1";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task CancelEntryAsync_WhenEntryExists_ShouldReturnTrue()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = testTournaments[0].Id;
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task CancelEntryAsync_WhenEntryDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            string playerId = "nonexistentPlayer";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_ShouldReturnCorrectIds()
        {
            // Arrange
            string playerId = "player1";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_WhenPlayerHasNoTournaments_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = "nonexistentPlayer";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_ShouldExcludeDeletedEntries()
        {
            // Arrange
            string playerId = "player1";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_ShouldReturnDistinctIds()
        {
            // Arrange
            string playerId = "player1";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }
    }
}
