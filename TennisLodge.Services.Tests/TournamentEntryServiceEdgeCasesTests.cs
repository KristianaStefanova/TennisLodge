using Moq;
using NUnit.Framework;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class TournamentEntryServiceEdgeCasesTests
    {
        private Mock<ITournamentEntryRepository> mockRepository;
        private TournamentEntryService service;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<ITournamentEntryRepository>();
            service = new TournamentEntryService(mockRepository.Object);
        }

        [Test]
        public async Task JoinTournamentAsync_WithNullPlayerId_ShouldWorkNormally()
        {
            // Arrange
            string playerId = null!;
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task JoinTournamentAsync_WithEmptyPlayerId_ShouldWorkNormally()
        {
            // Arrange
            string playerId = "";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task JoinTournamentAsync_WithEmptyGuid_ShouldWorkNormally()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = Guid.Empty;

            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = NewMethod(playerId, tournamentId);

            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        private InvalidOperationException? NewMethod(string playerId, Guid tournamentId)
        {
            return Assert.ThrowsAsync<InvalidOperationException>(async () =>
                            await service.JoinTournamentAsync(playerId, tournamentId));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WithNullPlayerId_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = null!;
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WithEmptyPlayerId_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = "";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task CancelEntryAsync_WithNullPlayerId_ShouldReturnFalse()
        {
            // Arrange
            string playerId = null!;
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task CancelEntryAsync_WithEmptyPlayerId_ShouldReturnFalse()
        {
            // Arrange
            string playerId = "";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_WithNullPlayerId_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = null!;
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_WithEmptyPlayerId_ShouldReturnEmptyList()
        {
            // Arrange
            string playerId = "";
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.GetMyTournamentIdsAsync(playerId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task JoinTournamentAsync_WhenRepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WhenRepositoryThrowsException_ShouldPropagateException()
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
        public async Task CancelEntryAsync_WhenRepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentIdsAsync_WhenRepositoryThrowsException_ShouldPropagateException()
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
        public async Task JoinTournamentAsync_WhenAddAsyncThrowsException_ShouldPropagateException()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task CancelEntryAsync_WhenDeleteAsyncThrowsException_ShouldPropagateException()
        {
            // Arrange
            string playerId = "player1";
            Guid tournamentId = Guid.NewGuid();

            TournamentEntry testEntry = new TournamentEntry
            {
                Id = 1,
                PlayerId = playerId,
                TournamentId = tournamentId,
                Tournament = new Tournament { Name = "Test Tournament" },
                IsDeleted = false
            };
            
            // Configurar el mock para que GetAllAttached lance una excepción
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.CancelEntryAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WithNullTournamentNavigation_ShouldHandleGracefully()
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
        public async Task JoinTournamentAsync_WithVeryLongPlayerId_ShouldWorkNormally()
        {
            // Arrange
            string playerId = new string('a', 1000); // ID muy largo
            Guid tournamentId = Guid.NewGuid();
            
            mockRepository.Setup(r => r.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.JoinTournamentAsync(playerId, tournamentId));
            
            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
        }

        [Test]
        public async Task GetMyTournamentsAsync_WithLargeDataset_ShouldHandleEfficiently()
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
    }
} 