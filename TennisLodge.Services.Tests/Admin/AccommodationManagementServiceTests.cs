using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core.Admin;
using TennisLodge.Web.ViewModels.Admin.AccommodationManagement;

namespace TennisLodge.Services.Tests.Admin
{
    [TestFixture]
    public class AccommodationManagementServiceTests
    {
        private Mock<IAccommodationRepository> mockAccommodationRepository;
        private AccommodationManagementService accommodationManagementService;

        [SetUp]
        public void Setup()
        {
            this.mockAccommodationRepository = new Mock<IAccommodationRepository>();
            this.accommodationManagementService = new AccommodationManagementService(mockAccommodationRepository.Object);
        }

        [Test]
        public async Task GetAllAccommodationsForAdminAsync_ShouldReturnAllAccommodations()
        {
            // Arrange
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
                    HostUser = new ApplicationUser { UserName = "user1" },
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
                    HostUser = new ApplicationUser { UserName = "user2" },
                    IsAvailable = false,
                    IsDeleted = true
                }
            };

            // Mock the repository to return the accommodations directly
            this.mockAccommodationRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(accommodations));

            // Act
            IEnumerable<AccommodationAdminListViewModel> result = await accommodationManagementService.GetAllAccommodationsForAdminAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].HostUsername, Is.EqualTo("user1"));
            Assert.That(resultList[1].HostUsername, Is.EqualTo("user2"));
        }

        [Test]
        public async Task RestoreAccommodationAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsDeleted = true,
                DeletedOn = DateTime.UtcNow
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(true);

            // Act
            bool result = await accommodationManagementService.RestoreAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.UpdateAsync(It.Is<Accommodation>(a => 
                a.IsDeleted == false && a.DeletedOn == null)), Times.Once);
        }

        [Test]
        public async Task RestoreAccommodationAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            bool result = await accommodationManagementService.RestoreAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RestoreAccommodationAsync_WithNonExistentAccommodation_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "999";

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            bool result = await accommodationManagementService.RestoreAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ActivateAccommodationAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsAvailable = false
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(true);

            // Act
            bool result = await accommodationManagementService.ActivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.UpdateAsync(It.Is<Accommodation>(a => 
                a.IsAvailable == true)), Times.Once);
        }

        [Test]
        public async Task ActivateAccommodationAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            bool result = await accommodationManagementService.ActivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ActivateAccommodationAsync_WithNonExistentAccommodation_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "999";

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            bool result = await accommodationManagementService.ActivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeactivateAccommodationAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsAvailable = true
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(true);

            // Act
            bool result = await accommodationManagementService.DeactivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.True);
            this.mockAccommodationRepository.Verify(x => x.UpdateAsync(It.Is<Accommodation>(a => 
                a.IsAvailable == false)), Times.Once);
        }

        [Test]
        public async Task DeactivateAccommodationAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "invalid-id";

            // Act
            bool result = await accommodationManagementService.DeactivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeactivateAccommodationAsync_WithNonExistentAccommodation_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "999";

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Accommodation?)null);

            // Act
            bool result = await accommodationManagementService.DeactivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RestoreAccommodationAsync_WithNullId_ShouldReturnFalse()
        {
            // Arrange
            string? accommodationId = null;

            // Act
            bool result = await accommodationManagementService.RestoreAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ActivateAccommodationAsync_WithNullId_ShouldReturnFalse()
        {
            // Arrange
            string? accommodationId = null;

            // Act
            bool result = await accommodationManagementService.ActivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeactivateAccommodationAsync_WithNullId_ShouldReturnFalse()
        {
            // Arrange
            string? accommodationId = null;

            // Act
            bool result = await accommodationManagementService.DeactivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RestoreAccommodationAsync_WhenUpdateFails_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsDeleted = true,
                DeletedOn = DateTime.UtcNow
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(false);

            // Act
            bool result = await accommodationManagementService.RestoreAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ActivateAccommodationAsync_WhenUpdateFails_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsAvailable = false
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(false);

            // Act
            bool result = await accommodationManagementService.ActivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeactivateAccommodationAsync_WhenUpdateFails_ShouldReturnFalse()
        {
            // Arrange
            string accommodationId = "1";
            Accommodation accommodation = new Accommodation
            {
                Id = 1,
                City = "Sofia",
                IsAvailable = true
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(accommodation);

            this.mockAccommodationRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Accommodation>()))
                .ReturnsAsync(false);

            // Act
            bool result = await accommodationManagementService.DeactivateAccommodationAsync(accommodationId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAllAccommodationsForAdminAsync_ShouldReturnEmptyList_WhenNoAccommodations()
        {
            // Arrange
            List<Accommodation> accommodations = new List<Accommodation>();

            this.mockAccommodationRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(accommodations));

            // Act
            IEnumerable<AccommodationAdminListViewModel> result = await accommodationManagementService.GetAllAccommodationsForAdminAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetAllAccommodationsForAdminAsync_ShouldHandleNullAvailableFromAndTo()
        {
            // Arrange
            var accommodations = new List<Accommodation>
            {
                new Accommodation
                {
                    Id = 1,
                    City = "Sofia",
                    Address = "Test Address",
                    MaxGuests = 2,
                    AvailableFrom = null,
                    AvailableTo = null,
                    HostUser = new ApplicationUser { UserName = "user1" },
                    IsAvailable = true,
                    IsDeleted = false
                }
            };

            this.mockAccommodationRepository
                .Setup(x => x.GetAllAttached())
                .Returns(CreateMockQueryableWithAsyncSupport(accommodations));

            // Act
            var result = await accommodationManagementService.GetAllAccommodationsForAdminAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0].AvailableFrom, Is.EqualTo("N/A"));
            Assert.That(resultList[0].AvailableTo, Is.EqualTo("N/A"));
        }

        #region Async Query Helper Classes
        private class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
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
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
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
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public T Current
            {
                get { return _inner.Current; }
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_inner.MoveNext());
            }

            public ValueTask DisposeAsync()
            {
                _inner.Dispose();
                return new ValueTask();
            }
        }

        private IQueryable<T> CreateMockQueryableWithAsyncSupport<T>(IEnumerable<T> data) where T : class
        {
            IQueryable<T> queryable = data.AsQueryable();
            Mock<IQueryable<T>> mockQueryable = new Mock<IQueryable<T>>();
            Mock<IAsyncEnumerable<T>> mockAsyncQueryable = new Mock<IAsyncEnumerable<T>>();

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
