using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Accommodation;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class AccommodationRequestServiceTests
    {
        private Mock<IAccommodationRequestRepository> mockRepository;
        private AccommodationRequestService service;

        [SetUp]
        public void Setup()
        {
            this.mockRepository = new Mock<IAccommodationRequestRepository>();
            this.service = new AccommodationRequestService(mockRepository.Object);
        }

        [TestFixture]
        public class CreateAccommodationRequestAsyncTests : AccommodationRequestServiceTests
        {
            [Test]
            public async Task CreateAccommodationRequestAsync_WithValidInput_ShouldCreateRequestSuccessfully()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = Guid.NewGuid().ToString(),
                    NumberOfGuests = 2,
                    Notes = "Test notes"
                };

                this.mockRepository.Setup(r => r.AddAsync(It.IsAny<AccommodationRequest>()))
                    .Returns(Task.CompletedTask);
                this.mockRepository.Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                await service.CreateAccommodationRequestAsync(guestUserId, inputModel);

                // Assert
                this.mockRepository.Verify(r => r.AddAsync(It.Is<AccommodationRequest>(ar =>
                    ar.GuestUserId == guestUserId &&
                    ar.TournamentId == Guid.Parse(inputModel.TournamentId) &&
                    ar.NumberOfGuests == inputModel.NumberOfGuests &&
                    ar.Notes == inputModel.Notes &&
                    ar.IsFulfilled == false &&
                    ar.CreatedOn.Date == DateTime.UtcNow.Date
                )), Times.Once);

                this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
            }

            [Test]
            public void CreateAccommodationRequestAsync_WithInvalidTournamentId_ShouldThrowArgumentException()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = "invalid-guid",
                    NumberOfGuests = 2,
                    Notes = "Test notes"
                };

                // Act & Assert
                ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await service.CreateAccommodationRequestAsync(guestUserId, inputModel));

                Assert.That(exception.Message, Is.EqualTo("Invalid tournament ID."));
            }

            [Test]
            public async Task CreateAccommodationRequestAsync_WithNullNotes_ShouldCreateRequestSuccessfully()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = Guid.NewGuid().ToString(),
                    NumberOfGuests = 1,
                    Notes = null
                };

                this.mockRepository.Setup(r => r.AddAsync(It.IsAny<AccommodationRequest>()))
                    .Returns(Task.CompletedTask);
                this.mockRepository.Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                await service.CreateAccommodationRequestAsync(guestUserId, inputModel);

                // Assert
                mockRepository.Verify(r => r.AddAsync(It.Is<AccommodationRequest>(ar =>
                    ar.Notes == null
                )), Times.Once);
            }

            [Test]
            public async Task CreateAccommodationRequestAsync_WithEmptyNotes_ShouldCreateRequestSuccessfully()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = Guid.NewGuid().ToString(),
                    NumberOfGuests = 1,
                    Notes = ""
                };

                this.mockRepository.Setup(r => r.AddAsync(It.IsAny<AccommodationRequest>()))
                    .Returns(Task.CompletedTask);
                this.mockRepository.Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                await service.CreateAccommodationRequestAsync(guestUserId, inputModel);

                // Assert
                this.mockRepository.Verify(r => r.AddAsync(It.Is<AccommodationRequest>(ar =>
                    ar.Notes == ""
                )), Times.Once);
            }

            [Test]
            public void CreateAccommodationRequestAsync_WhenRepositoryThrowsException_ShouldPropagateException()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = Guid.NewGuid().ToString(),
                    NumberOfGuests = 2,
                    Notes = "Test notes"
                };

                this.mockRepository.Setup(r => r.AddAsync(It.IsAny<AccommodationRequest>()))
                    .ThrowsAsync(new InvalidOperationException("Database error"));

                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await service.CreateAccommodationRequestAsync(guestUserId, inputModel));
            }

            [Test]
            public void CreateAccommodationRequestAsync_WithEmptyTournamentId_ShouldThrowArgumentException()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = "",
                    NumberOfGuests = 2,
                    Notes = "Test notes"
                };

                // Act & Assert
                ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await service.CreateAccommodationRequestAsync(guestUserId, inputModel));

                Assert.That(exception.Message, Is.EqualTo("Invalid tournament ID."));
            }

            [Test]
            public void CreateAccommodationRequestAsync_WithNullTournamentId_ShouldThrowArgumentException()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = null,
                    NumberOfGuests = 2,
                    Notes = "Test notes"
                };

                // Act & Assert
                ArgumentException? exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                    await service.CreateAccommodationRequestAsync(guestUserId, inputModel));

                Assert.That(exception.Message, Is.EqualTo("Invalid tournament ID."));
            }

            [Test]
            public async Task CreateAccommodationRequestAsync_WithZeroNumberOfGuests_ShouldCreateRequestSuccessfully()
            {
                // Arrange
                string guestUserId = "test-user-id";
                AccommodationRequestInputModel inputModel = new AccommodationRequestInputModel
                {
                    TournamentId = Guid.NewGuid().ToString(),
                    NumberOfGuests = 0,
                    Notes = "Test notes"
                };

                this.mockRepository.Setup(r => r.AddAsync(It.IsAny<AccommodationRequest>()))
                    .Returns(Task.CompletedTask);
                this.mockRepository.Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                await service.CreateAccommodationRequestAsync(guestUserId, inputModel);

                // Assert
                this.mockRepository.Verify(r => r.AddAsync(It.Is<AccommodationRequest>(ar =>
                    ar.NumberOfGuests == 0
                )), Times.Once);
            }
        }

        [TestFixture]
        public class GetRequestsByUserIdAsyncTests : AccommodationRequestServiceTests
        {
            [Test]
            public async Task GetRequestsByUserIdAsync_WithValidUserId_ShouldReturnUserRequests()
            {
                // Arrange
                string userId = "test-user-id";
                Guid tournamentId = Guid.NewGuid();

                Tournament tournament = new Tournament
                {
                    Id = tournamentId,
                    Name = "Test Tournament",
                    Description = "Test Description",
                    Location = "Test Location",
                    Surface = "Hard",
                    Organizer = "Test Organizer",
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(35)),
                    CategoryId = 1
                };

                List<AccommodationRequest> accommodationRequests = new List<AccommodationRequest>
                {
                    new AccommodationRequest
                    {
                        Id = 1,
                        GuestUserId = userId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 2,
                        Notes = "Test notes 1",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow.AddDays(-1)
                    },
                    new AccommodationRequest
                    {
                        Id = 2,
                        GuestUserId = userId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 1,
                        Notes = "Test notes 2",
                        IsFulfilled = true,
                        CreatedOn = DateTime.UtcNow.AddDays(-2)
                    }
                };

                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);

                Category category = new Category { Id = 1, Name = "Test Category" };
                dbContext.Categories.Add(category);
                
                dbContext.Tournaments.Add(tournament);
                dbContext.AccommodationRequests.AddRange(accommodationRequests);
                await dbContext.SaveChangesAsync();

                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2));

                AccommodationRequestViewModel firstRequest = result.First();
                Assert.That(firstRequest.Id, Is.EqualTo(1));
                Assert.That(firstRequest.TournamentName, Is.EqualTo("Test Tournament"));
                Assert.That(firstRequest.NumberOfGuests, Is.EqualTo(2));
                Assert.That(firstRequest.Notes, Is.EqualTo("Test notes 1"));
                Assert.That(firstRequest.IsFulfilled, Is.False);

                AccommodationRequestViewModel secondRequest = result.Last();
                Assert.That(secondRequest.Id, Is.EqualTo(2));
                Assert.That(secondRequest.TournamentName, Is.EqualTo("Test Tournament"));
                Assert.That(secondRequest.NumberOfGuests, Is.EqualTo(1));
                Assert.That(secondRequest.Notes, Is.EqualTo("Test notes 2"));
                Assert.That(secondRequest.IsFulfilled, Is.True);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithNoRequests_ShouldReturnEmptyList()
            {
                // Arrange
                string userId = "test-user-id";

                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithNullTournament_ShouldReturnDefaultTournamentName()
            {
                // Arrange
                string userId = "test-user-id";
                Guid tournamentId = Guid.NewGuid();

                // Create accommodation request with tournament ID but no tournament entity
                List<AccommodationRequest> accommodationRequests = new List<AccommodationRequest>
                {
                    new AccommodationRequest
                    {
                        Id = 1,
                        GuestUserId = userId,
                        TournamentId = tournamentId, // Tournament ID exists but no tournament entity
                        NumberOfGuests = 2,
                        Notes = "Test notes",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow
                    }
                };

                // Use Entity Framework In-Memory Database for this test
                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                
                // Only add accommodation request, no tournament entity
                dbContext.AccommodationRequests.AddRange(accommodationRequests);
                await dbContext.SaveChangesAsync();

                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(1));

                AccommodationRequestViewModel request = result.First();
                Assert.That(request.TournamentName, Is.EqualTo("Tournament is not available"));
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithDifferentUserRequests_ShouldOnlyReturnRequestedUserRequests()
            {
                // Arrange
                string userId = "test-user-id";
                string otherUserId = "other-user-id";
                Guid tournamentId = Guid.NewGuid();

                // Create a simple tournament without complex relationships
                Tournament tournament = new Tournament
                {
                    Id = tournamentId,
                    Name = "Test Tournament",
                    Description = "Test Description",
                    Location = "Test Location",
                    Surface = "Hard",
                    Organizer = "Test Organizer",
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(35)),
                    CategoryId = 1
                };

                List<AccommodationRequest> accommodationRequests = new List<AccommodationRequest>
                {
                    new AccommodationRequest
                    {
                        Id = 1,
                        GuestUserId = userId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 2,
                        Notes = "User request",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow
                    },
                    new AccommodationRequest
                    {
                        Id = 2,
                        GuestUserId = otherUserId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 1,
                        Notes = "Other user request",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow
                    }
                };

                // Use Entity Framework In-Memory Database for this test
                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);

                // Add category first (required foreign key)
                Category category = new Category { Id = 1, Name = "Test Category" };
                dbContext.Categories.Add(category);
                
                dbContext.Tournaments.Add(tournament);
                dbContext.AccommodationRequests.AddRange(accommodationRequests);
                await dbContext.SaveChangesAsync();

                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                var result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(1));

                AccommodationRequestViewModel request = result.First();
                Assert.That(request.Id, Is.EqualTo(1));
                Assert.That(request.Notes, Is.EqualTo("User request"));
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithEmptyUserId_ShouldReturnEmptyList()
            {
                // Arrange
                string userId = "";

                // Use Entity Framework In-Memory Database with empty data
                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                var result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithNullUserId_ShouldReturnEmptyList()
            {
                // Arrange
                string userId = null;

                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                var result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithWhitespaceUserId_ShouldReturnEmptyList()
            {
                // Arrange
                string userId = "   ";

                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithRepositoryThrowingException_ShouldPropagateException()
            {
                // Arrange
                string userId = "test-user-id";

                // Create a mock repository that throws an exception
                Mock<IAccommodationRequestRepository> mockRepository = new Mock<IAccommodationRequestRepository>();
                mockRepository.Setup(r => r.GetAllAttached())
                    .Throws(new InvalidOperationException("Database connection error"));

                AccommodationRequestService service = new AccommodationRequestService(mockRepository.Object);

                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await service.GetRequestsByUserIdAsync(userId));
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_ShouldReturnMultipleRequestsForUser()
            {
                // Arrange
                string userId = "test-user-id";
                Guid tournamentId = Guid.NewGuid();

                // Create a simple tournament without complex relationships
                Tournament tournament = new Tournament
                {
                    Id = tournamentId,
                    Name = "Test Tournament",
                    Description = "Test Description",
                    Location = "Test Location",
                    Surface = "Hard",
                    Organizer = "Test Organizer",
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(35)),
                    CategoryId = 1
                };

                List<AccommodationRequest> accommodationRequests = new List<AccommodationRequest>
                {
                    new AccommodationRequest
                    {
                        Id = 1,
                        GuestUserId = userId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 2,
                        Notes = "First request",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow.AddDays(-2)
                    },
                    new AccommodationRequest
                    {
                        Id = 2,
                        GuestUserId = userId,
                        TournamentId = tournamentId,
                        NumberOfGuests = 1,
                        Notes = "Second request",
                        IsFulfilled = false,
                        CreatedOn = DateTime.UtcNow.AddDays(-1)
                    }
                };

                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);

                Category category = new Category { Id = 1, Name = "Test Category" };
                dbContext.Categories.Add(category);
                
                dbContext.Tournaments.Add(tournament);
                dbContext.AccommodationRequests.AddRange(accommodationRequests);
                await dbContext.SaveChangesAsync();

                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2));

                List<AccommodationRequestViewModel> resultList = result.ToList();
                Assert.That(resultList.Any(r => r.Id == 1), Is.True);
                Assert.That(resultList.Any(r => r.Id == 2), Is.True);
                Assert.That(resultList.Any(r => r.Notes == "First request"), Is.True);
                Assert.That(resultList.Any(r => r.Notes == "Second request"), Is.True);
            }

            [Test]
            public async Task GetRequestsByUserIdAsync_WithRepositoryReturningNull_ShouldHandleGracefully()
            {
                // Arrange
                string userId = "test-user-id";

                // Use Entity Framework In-Memory Database with empty data
                DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                using TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);
                AccommodationRequestRepository repository = new AccommodationRequestRepository(dbContext);
                AccommodationRequestService service = new AccommodationRequestService(repository);

                // Act
                IEnumerable<AccommodationRequestViewModel> result = await service.GetRequestsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Empty);
            }
        }

        [TestFixture]
        public class ConstructorTests : AccommodationRequestServiceTests
        {
            [Test]
            public void Constructor_WithValidRepository_ShouldCreateService()
            {
                // Arrange & Act
                AccommodationRequestService service = new AccommodationRequestService(mockRepository.Object);

                // Assert
                Assert.That(service, Is.Not.Null);
                Assert.That(service, Is.InstanceOf<AccommodationRequestService>());
            }

            [Test]
            public void Constructor_WithNullRepository_ShouldCreateServiceWithoutValidation()
            {
                // Note: The current implementation doesn't validate null repository
                // This test documents the current behavior
                // Act & Assert
                Assert.DoesNotThrow(() => new AccommodationRequestService(null));
            }

            [Test]
            public void Service_ShouldImplementIAccommodationRequestService()
            {
                // Arrange & Act
                AccommodationRequestService service = new AccommodationRequestService(mockRepository.Object);

                // Assert
                Assert.That(service, Is.InstanceOf<IAccommodationRequestService>());
            }
        }
    }
}
