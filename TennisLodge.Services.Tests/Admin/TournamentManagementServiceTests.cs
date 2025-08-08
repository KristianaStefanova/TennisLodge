using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Admin;
using TennisLodge.Services.Core.Interfaces;
using TennisLodge.Web.ViewModels.Admin.TournamentManagement;
using static TennisLodge.GCommon.ApplicationConstants;

namespace TennisLodge.Services.Tests.Admin
{
    [TestFixture]
    public class TournamentManagementServiceTests
    {
        private Mock<ICategoryRepository> mockCategoryRepository;
        private Mock<ITournamentRepository> mockTournamentRepository;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<IFavoriteService> mockFavoriteService;
        private TournamentManagementService tournamentManagementService;

        [SetUp]
        public void Setup()
        {
            this.mockCategoryRepository = new Mock<ICategoryRepository>();
            this.mockTournamentRepository = new Mock<ITournamentRepository>();
            this.mockFavoriteService = new Mock<IFavoriteService>();
            
            // Crear un UserManager mock
            Mock<IUserStore<ApplicationUser>> userStore = new Mock<IUserStore<ApplicationUser>>();
            this.mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            this.tournamentManagementService = new TournamentManagementService(
                mockCategoryRepository.Object, 
                mockTournamentRepository.Object, 
                mockUserManager.Object,
                mockFavoriteService.Object);
        }

        [Test]
        public async Task AddTournamentAsync_WithValidInput_ShouldAddTournament()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Hard",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = "/images/test.jpg",
                StartDate = "2025-10-01",
                EndDate = "2025-10-07"
            };
            string userId = "test-user-id";

            this.mockTournamentRepository
                .Setup(x => x.AddAsync(It.IsAny<Tournament>()))
                .Returns(Task.CompletedTask);

            // Act
            await tournamentManagementService.AddTournamentAsync(inputModel, userId);

            // Assert
            this.mockTournamentRepository.Verify(x => x.AddAsync(It.Is<Tournament>(t => 
                t.Name == inputModel.Name &&
                t.Description == inputModel.Description &&
                t.Location == inputModel.Location &&
                t.Surface == inputModel.Surface &&
                t.CategoryId == inputModel.CategoryId &&
                t.Organizer == inputModel.Organizer &&
                t.ImageUrl == inputModel.ImageUrl &&
                t.PublisherId == userId)), Times.Once);
        }

        [Test]
        public async Task AddTournamentAsync_WithNullInput_ShouldNotAddTournament()
        {
            // Arrange
            TournamentFormInputModel? inputModel = null;
            string userId = "test-user-id";

            // Act
            await tournamentManagementService.AddTournamentAsync(inputModel, userId);

            // Assert
            this.mockTournamentRepository.Verify(x => x.AddAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task AddTournamentAsync_WithEmptyUserId_ShouldNotAddTournament()
        {
            // Arrange
            var inputModel = new TournamentFormInputModel
            {
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Hard",
                CategoryId = 1,
                Organizer = "Test Organizer",
                StartDate = "2025-10-01",
                EndDate = "2025-10-07"
            };
            string userId = "";

            // Act
            await tournamentManagementService.AddTournamentAsync(inputModel, userId);

            // Assert
            this.mockTournamentRepository.Verify(x => x.AddAsync(It.IsAny<Tournament>()), Times.Never);
        }

        [Test]
        public async Task GetTournamentManagementBoardDataAsync_ShouldReturnAllTournaments()
        {
            // Arrange
            List<Tournament> tournaments = CreateTestTournaments();

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(tournaments));

            // Act
            IEnumerable<TournamentManagementIndexViewModel> result = await tournamentManagementService.GetTournamentManagementBoardDataAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            List<TournamentManagementIndexViewModel> resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].Name, Is.EqualTo("Tournament 1"));
            Assert.That(resultList[1].Name, Is.EqualTo("Tournament 2"));
        }

        [Test]
        public async Task GetTournamentManagementBoardDataAsync_ShouldReturnEmptyList_WhenNoTournaments()
        {
            // Arrange
            List<Tournament> tournaments = new List<Tournament>();

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(tournaments));

            // Act
            IEnumerable<TournamentManagementIndexViewModel> result = await tournamentManagementService.GetTournamentManagementBoardDataAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetTournamentEditFormModelAsync_WithValidId_ShouldReturnFormModel()
        {
            // Arrange
            string tournamentId = "8f19c979-40c2-4cb8-8af0-d061456245bd";
            Tournament tournament = CreateTestTournament();

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament> { tournament }));

            // Act
            TournamentFormInputModel? result = await tournamentManagementService.GetTournamentEditFormModelAsync(tournamentId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(tournament.Name));
            Assert.That(result.Description, Is.EqualTo(tournament.Description));
            Assert.That(result.Location, Is.EqualTo(tournament.Location));
            Assert.That(result.Surface, Is.EqualTo(tournament.Surface));
            Assert.That(result.CategoryId, Is.EqualTo(tournament.CategoryId));
            Assert.That(result.Organizer, Is.EqualTo(tournament.Organizer));
            Assert.That(result.ImageUrl, Is.EqualTo(tournament.ImageUrl));
        }

        [Test]
        public async Task GetTournamentEditFormModelAsync_WithNullId_ShouldReturnNull()
        {
            // Arrange
            string? tournamentId = null;

            // Act
            TournamentFormInputModel? result = await tournamentManagementService.GetTournamentEditFormModelAsync(tournamentId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetTournamentEditFormModelAsync_WithEmptyId_ShouldReturnNull()
        {
            // Arrange
            string tournamentId = "";

            // Act
            TournamentFormInputModel? result = await tournamentManagementService.GetTournamentEditFormModelAsync(tournamentId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetTournamentEditFormModelAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            string tournamentId = "non-existent-id";
            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament>()));

            // Act
            TournamentFormInputModel? result = await tournamentManagementService.GetTournamentEditFormModelAsync(tournamentId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditTournamentAsync_WithValidInput_ShouldReturnTrue()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = "8f19c979-40c2-4cb8-8af0-d061456245bd",
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Clay",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                ImageUrl = "/images/updated.jpg",
                StartDate = "2025-10-15",
                EndDate = "2025-10-21"
            };
            string userId = "test-user-id";
            Tournament tournament = CreateTestTournament();

            this.mockTournamentRepository
                .Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Tournament, bool>>>()))
                .ReturnsAsync(tournament);

            this.mockTournamentRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            bool result = await tournamentManagementService.EditTournamentAsync(inputModel, userId);

            // Assert
            Assert.That(result, Is.True);
            this.mockTournamentRepository.Verify(x => x.UpdateAsync(It.Is<Tournament>(t => 
                t.Name == inputModel.Name &&
                t.Description == inputModel.Description &&
                t.Location == inputModel.Location &&
                t.Surface == inputModel.Surface &&
                t.CategoryId == inputModel.CategoryId &&
                t.Organizer == inputModel.Organizer &&
                t.ImageUrl == inputModel.ImageUrl)), Times.Once);
        }

        [Test]
        public async Task EditTournamentAsync_WithNullInput_ShouldReturnFalse()
        {
            // Arrange
            TournamentFormInputModel? inputModel = null;
            string userId = "test-user-id";

            // Act
            bool result = await tournamentManagementService.EditTournamentAsync(inputModel, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditTournamentAsync_WithEmptyUserId_ShouldReturnFalse()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = "8f19c979-40c2-4cb8-8af0-d061456245bd",
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Clay",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                StartDate = "2025-10-15",
                EndDate = "2025-10-21"
            };
            string userId = "";

            // Act
            bool result = await tournamentManagementService.EditTournamentAsync(inputModel, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditTournamentAsync_WithNonExistentTournament_ShouldReturnFalse()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = "non-existent-id",
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Clay",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                StartDate = "2025-10-15",
                EndDate = "2025-10-21"
            };
            string userId = "test-user-id";

            this.mockTournamentRepository
                .Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Tournament, bool>>>()))
                .ReturnsAsync((Tournament?)null);

            // Act
            bool result = await tournamentManagementService.EditTournamentAsync(inputModel, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task EditTournamentAsync_WhenUpdateFails_ShouldReturnFalse()
        {
            // Arrange
            TournamentFormInputModel inputModel = new TournamentFormInputModel
            {
                Id = "8f19c979-40c2-4cb8-8af0-d061456245bd",
                Name = "Updated Tournament",
                Description = "Updated Description",
                Location = "Updated Location",
                Surface = "Clay",
                CategoryId = 2,
                Organizer = "Updated Organizer",
                StartDate = "2025-10-15",
                EndDate = "2025-10-21"
            };
            string userId = "test-user-id";
            Tournament tournament = CreateTestTournament();

            this.mockTournamentRepository
                .Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Tournament, bool>>>()))
                .ReturnsAsync(tournament);

            this.mockTournamentRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(false);

            // Act
            bool result = await tournamentManagementService.EditTournamentAsync(inputModel, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WithValidId_ShouldDeleteTournament()
        {
            // Arrange
            string tournamentId = "8f19c979-40c2-4cb8-8af0-d061456245bd";
            Tournament tournament = CreateTestTournament();
            tournament.IsDeleted = false;

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament> { tournament }));

            this.mockTournamentRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            var result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.True); // Success
            Assert.That(result.Item2, Is.False); // Not restored
            this.mockTournamentRepository.Verify(x => x.UpdateAsync(It.Is<Tournament>(t => 
                t.IsDeleted == true)), Times.Once);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WithDeletedTournament_ShouldRestoreTournament()
        {
            // Arrange
            string tournamentId = "8f19c979-40c2-4cb8-8af0-d061456245bd";
            Tournament tournament = CreateTestTournament();
            tournament.IsDeleted = true;

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament> { tournament }));

            this.mockTournamentRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(true);

            // Act
            Tuple<bool, bool> result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.True); // Success
            Assert.That(result.Item2, Is.True); // Restored
            this.mockTournamentRepository.Verify(x => x.UpdateAsync(It.Is<Tournament>(t => 
                t.IsDeleted == false)), Times.Once);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WithNullId_ShouldReturnFalse()
        {
            // Arrange
            string? tournamentId = null;

            // Act
            Tuple<bool, bool> result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result.Item2, Is.False);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WithEmptyId_ShouldReturnFalse()
        {
            // Arrange
            string tournamentId = "";

            // Act
            Tuple<bool, bool> result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result.Item2, Is.False);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WithNonExistentTournament_ShouldReturnFalse()
        {
            // Arrange
            string tournamentId = "non-existent-id";
            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament>()));

            // Act
            Tuple<bool, bool> result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result.Item2, Is.False);
        }

        [Test]
        public async Task DeleteOrRestoreTournamentAsync_WhenUpdateFails_ShouldReturnFalse()
        {
            // Arrange
            string tournamentId = "8f19c979-40c2-4cb8-8af0-d061456245bd";
            var tournament = CreateTestTournament();
            tournament.IsDeleted = false;

            this.mockTournamentRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(new List<Tournament> { tournament }));

            this.mockTournamentRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Tournament>()))
                .ReturnsAsync(false);

            // Act
            Tuple<bool, bool> result = await tournamentManagementService.DeleteOrRestoreTournamentAsync(tournamentId);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result.Item2, Is.False);
        }

        #region Helper Methods
        private static List<Tournament> CreateTestTournaments()
        {
            return new List<Tournament>
            {
                new Tournament
                {
                    Id = Guid.Parse("8f19c979-40c2-4cb8-8af0-d061456245bd"),
                    Name = "Tournament 1",
                    Description = "Description 1",
                    Location = "Location 1",
                    Surface = "Hard",
                    CategoryId = 1,
                    Organizer = "Organizer 1",
                    ImageUrl = "/images/tournament1.jpg",
                    StartDate = new DateOnly(2025, 10, 1),
                    EndDate = new DateOnly(2025, 10, 7),
                    PublisherId = "publisher1",
                    Category = new Category { Name = "ATP 250" },
                    Publisher = new ApplicationUser { UserName = "admin1" },
                    UserTournaments = new List<UserTournament>(),
                    AccommodationRequests = new List<AccommodationRequest>(),
                    IsDeleted = false
                },
                new Tournament
                {
                    Id = Guid.Parse("7b6d0baf-2a39-41d7-bc2a-e5713b302baf"),
                    Name = "Tournament 2",
                    Description = "Description 2",
                    Location = "Location 2",
                    Surface = "Clay",
                    CategoryId = 2,
                    Organizer = "Organizer 2",
                    ImageUrl = "/images/tournament2.jpg",
                    StartDate = new DateOnly(2025, 6, 10),
                    EndDate = new DateOnly(2025, 6, 16),
                    PublisherId = "publisher2",
                    Category = new Category { Name = "Challenger" },
                    Publisher = new ApplicationUser { UserName = "admin2" },
                    UserTournaments = new List<UserTournament>(),
                    AccommodationRequests = new List<AccommodationRequest>(),
                    IsDeleted = true
                }
            };
        }

        private static Tournament CreateTestTournament()
        {
            return new Tournament
            {
                Id = Guid.Parse("8f19c979-40c2-4cb8-8af0-d061456245bd"),
                Name = "Test Tournament",
                Description = "Test Description",
                Location = "Test Location",
                Surface = "Hard",
                CategoryId = 1,
                Organizer = "Test Organizer",
                ImageUrl = "/images/test.jpg",
                StartDate = new DateOnly(2025, 10, 1),
                EndDate = new DateOnly(2025, 10, 7),
                PublisherId = "test-publisher",
                Category = new Category { Name = "ATP 250" },
                Publisher = new ApplicationUser { UserName = "test-admin" },
                UserTournaments = new List<UserTournament>(),
                AccommodationRequests = new List<AccommodationRequest>(),
                IsDeleted = false
            };
        }


        #endregion

        #region Async Query Helper Classes
        private class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider inner;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                this.inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object? Execute(Expression expression)
            {
                return inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return inner.Execute<TResult>(expression);
            }

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
            {
                var resultType = typeof(TResult).GetGenericArguments()[0];
                var executionResult = typeof(IQueryProvider)
                    .GetMethod(
                        name: nameof(IQueryProvider.Execute),
                        genericParameterCount: 1,
                        types: new[] { typeof(Expression) })
                    ?.MakeGenericMethod(resultType)
                    ?.Invoke(this, new[] { expression });

                return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                    ?.MakeGenericMethod(resultType)
                    ?.Invoke(null, new[] { executionResult })!;
            }
        }

        private class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }
        }

        private class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                this.inner = inner;
            }

            public void Dispose()
            {
                inner.Dispose();
            }

            public T Current
            {
                get { return inner.Current; }
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(inner.MoveNext());
            }

            public ValueTask DisposeAsync()
            {
                inner.Dispose();
                return new ValueTask();
            }
        }

        private IQueryable<T> CreateMockQueryableWithAsyncSupport<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockQueryable = new Mock<IQueryable<T>>();
            var mockAsyncQueryable = new Mock<IAsyncEnumerable<T>>();

            mockQueryable.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(() => new TestAsyncEnumerator<T>(data.GetEnumerator()));

            mockQueryable.Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
            mockQueryable.Setup(m => m.Expression).Returns(queryable.Expression);
            mockQueryable.Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockQueryable.Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return mockQueryable.Object;
        }
        #endregion
    }


}
