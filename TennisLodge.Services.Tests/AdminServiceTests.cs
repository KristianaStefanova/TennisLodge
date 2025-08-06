using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private Mock<IAdminRepository> mockAdminRepository;
        private AdminService adminService;

        [SetUp]
        public void Setup()
        {
            mockAdminRepository = new Mock<IAdminRepository>();
            adminService = new AdminService(mockAdminRepository.Object);
        }

        [TestFixture]
        public class ExistsByUserIdAsyncTests : AdminServiceTests
        {
            [Test]
            public async Task ExistsByUserIdAsync_WithValidUserId_ReturnsTrue()
            {
                // Arrange
                string userId = "test-user-id";
                var mockQueryable = CreateMockQueryableWithAdmin(userId);
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.True);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }

            [Test]
            public async Task ExistsByUserIdAsync_WithNonExistentUserId_ReturnsFalse()
            {
                // Arrange
                string userId = "non-existent-user-id";
                IQueryable<Admin> mockQueryable = CreateMockQueryableWithAdmin("different-user-id");
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }

            [Test]
            public async Task ExistsByUserIdAsync_WithNullUserId_ReturnsFalse()
            {
                // Arrange
                string? userId = null;

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByUserIdAsync_WithEmptyUserId_ReturnsFalse()
            {
                // Arrange
                string userId = "";

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByUserIdAsync_WithWhitespaceUserId_ReturnsFalse()
            {
                // Arrange
                string userId = "   ";

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByUserIdAsync_WithCaseInsensitiveMatch_ReturnsTrue()
            {
                // Arrange
                string userId = "TEST-USER-ID";
                var mockQueryable = CreateMockQueryableWithAdmin("test-user-id");
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.True);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }
        }

        [TestFixture]
        public class ExistsByIdAsyncTests : AdminServiceTests
        {
            [Test]
            public async Task ExistsByIdAsync_WithValidId_ReturnsTrue()
            {
                // Arrange
                string id = "test-admin-id";
                IQueryable<Admin> mockQueryable = CreateMockQueryableWithAdminId(id);
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.True);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }

            [Test]
            public async Task ExistsByIdAsync_WithNonExistentId_ReturnsFalse()
            {
                // Arrange
                string id = "non-existent-admin-id";
                IQueryable<Admin> mockQueryable = CreateMockQueryableWithAdminId("different-admin-id");
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }

            [Test]
            public async Task ExistsByIdAsync_WithNullId_ReturnsFalse()
            {
                // Arrange
                string? id = null;

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByIdAsync_WithEmptyId_ReturnsFalse()
            {
                // Arrange
                string id = "";

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByIdAsync_WithWhitespaceId_ReturnsFalse()
            {
                // Arrange
                string id = "   ";

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.False);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Never);
            }

            [Test]
            public async Task ExistsByIdAsync_WithCaseInsensitiveMatch_ReturnsTrue()
            {
                // Arrange
                string id = "TEST-ADMIN-ID";
                var mockQueryable = CreateMockQueryableWithAdminId("test-admin-id");
                mockAdminRepository.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

                // Act
                bool result = await adminService.ExistsByIdAsync(id);

                // Assert
                Assert.That(result, Is.True);
                mockAdminRepository.Verify(r => r.GetAllAttached(), Times.Once);
            }
        }

        [TestFixture]
        public class GetIdByUserIdAsyncTests : AdminServiceTests
        {
            [Test]
            public async Task GetIdByUserIdAsync_WithValidUserId_ReturnsAdminId()
            {
                // Arrange
                string userId = "test-user-id";
                string expectedAdminId = "admin-id-123";
                Admin admin = new Admin { Id = expectedAdminId, UserId = userId };
                mockAdminRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()))
                    .ReturnsAsync(admin);

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.EqualTo(expectedAdminId));
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Once);
            }

            [Test]
            public async Task GetIdByUserIdAsync_WithNonExistentUserId_ReturnsNull()
            {
                // Arrange
                string userId = "non-existent-user-id";
                mockAdminRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()))
                    .ReturnsAsync((Admin?)null);

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Null);
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Once);
            }

            [Test]
            public async Task GetIdByUserIdAsync_WithNullUserId_ReturnsNull()
            {
                // Arrange
                string? userId = null;

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Null);
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Never);
            }

            [Test]
            public async Task GetIdByUserIdAsync_WithEmptyUserId_ReturnsNull()
            {
                // Arrange
                string userId = "";

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Null);
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Never);
            }

            [Test]
            public async Task GetIdByUserIdAsync_WithWhitespaceUserId_ReturnsNull()
            {
                // Arrange
                string userId = "   ";

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.Null);
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Never);
            }

            [Test]
            public async Task GetIdByUserIdAsync_WithCaseInsensitiveMatch_ReturnsAdminId()
            {
                // Arrange
                string userId = "TEST-USER-ID";
                string expectedAdminId = "admin-id-123";
                Admin admin = new Admin { Id = expectedAdminId, UserId = "test-user-id" };
                mockAdminRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()))
                    .ReturnsAsync(admin);

                // Act
                string? result = await adminService.GetIdByUserIdAsync(userId);

                // Assert
                Assert.That(result, Is.EqualTo(expectedAdminId));
                mockAdminRepository.Verify(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Admin, bool>>>()), Times.Once);
            }
        }

        #region Helper Methods
        private static IQueryable<Admin> CreateMockQueryableWithAdmin(string userId)
        {
            List<Admin> admins = new List<Admin>
            {
                new Admin { Id = "admin-1", UserId = userId, IsDeleted = false },
                new Admin { Id = "admin-2", UserId = "other-user", IsDeleted = false }
            };

            Mock<IQueryable<Admin>> mockQueryable = new Mock<IQueryable<Admin>>();
            mockQueryable.Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Admin>(admins.AsQueryable().Provider));
            mockQueryable.Setup(m => m.Expression).Returns(admins.AsQueryable().Expression);
            mockQueryable.Setup(m => m.ElementType).Returns(admins.AsQueryable().ElementType);
            mockQueryable.Setup(m => m.GetEnumerator()).Returns(admins.AsQueryable().GetEnumerator());
            
            return mockQueryable.Object;
        }

        private static IQueryable<Admin> CreateMockQueryableWithAdminId(string id)
        {
            List<Admin> admins = new List<Admin>
            {
                new Admin { Id = id, UserId = "user-1", IsDeleted = false },
                new Admin { Id = "other-admin", UserId = "user-2", IsDeleted = false }
            };

            Mock<IQueryable<Admin>> mockQueryable = new Mock<IQueryable<Admin>>();
            mockQueryable.Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Admin>(admins.AsQueryable().Provider));
            mockQueryable.Setup(m => m.Expression).Returns(admins.AsQueryable().Expression);
            mockQueryable.Setup(m => m.ElementType).Returns(admins.AsQueryable().ElementType);
            mockQueryable.Setup(m => m.GetEnumerator()).Returns(admins.AsQueryable().GetEnumerator());
            
            return mockQueryable.Object;
        }
        #endregion
    }

    public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            this.inner = inner;
        }

        public TestAsyncQueryProvider(Expression expression)
        {
            IQueryable<TEntity> tempQueryable = new List<TEntity>().AsQueryable();
            this.inner = tempQueryable.Provider;
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
            Type resultType = typeof(TResult).GetGenericArguments()[0];
            object? executionResult = typeof(IQueryProvider)
                .GetMethod(
                    name: nameof(IQueryProvider.Execute),
                    genericParameterCount: 1,
                    types: new[] { typeof(Expression) })
                ?.MakeGenericMethod(resultType)
                .Invoke(this, new[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(resultType)
                .Invoke(null, new[] { executionResult })!;
        }
    }

    public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        private readonly IQueryProvider provider;

        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
            provider = new TestAsyncQueryProvider<T>(enumerable.AsQueryable().Provider);
        }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        {
            provider = new TestAsyncQueryProvider<T>(expression);
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return provider; }
        }
    }

    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
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
}
