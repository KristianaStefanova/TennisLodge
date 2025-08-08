using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using TennisLodge.Data;
using TennisLodge.Data.Models;
using TennisLodge.Services.Core.Admin;
using TennisLodge.Web.ViewModels.Admin.UserManagement;

namespace TennisLodge.Services.Tests.AdminTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private UserService userService;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            TennisLodgeDbContext dbContext = new TennisLodgeDbContext(options);

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(dbContext);
            this.mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore, null, null, null, null, null, null, null, null);

            this.userService = new UserService(mockUserManager.Object, dbContext);
        }

        [TestFixture]
        public class GetUsersManagementBoardDataAsyncTests : UserServiceTests
        {
            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithValidUserId_ReturnsFilteredUsers()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2)); // Debería excluir al usuario actual
                Assert.That(result.Any(u => u.Id == currentUserId), Is.False);
                this.mockUserManager.Verify(um => um.Users, Times.Once);
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithCaseInsensitiveUserId_ExcludesUserCorrectly()
            {
                // Arrange
                string currentUserId = "CURRENT-USER-ID";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2)); // Debería excluir al usuario actual
                Assert.That(result.Any(u => u.Id.ToLower() == currentUserId.ToLower()), Is.False);
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithUsersHavingRoles_ReturnsCorrectRoleData()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == "user1")))
                    .ReturnsAsync(new List<string> { "Admin", "User" });
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == "user2")))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                UserManagementIndexViewModel? user1 = result.FirstOrDefault(u => u.Id == "user1");
                UserManagementIndexViewModel? user2 = result.FirstOrDefault(u => u.Id == "user2");

                Assert.That(user1, Is.Not.Null);
                Assert.That(user1.Roles, Is.Not.Null);
                Assert.That(user1.Roles.Count(), Is.EqualTo(2));
                Assert.That(user1.Roles.Contains("Admin"), Is.True);
                Assert.That(user1.Roles.Contains("User"), Is.True);

                Assert.That(user2, Is.Not.Null);
                Assert.That(user2.Roles, Is.Not.Null);
                Assert.That(user2.Roles.Count(), Is.EqualTo(1));
                Assert.That(user2.Roles.Contains("User"), Is.True);
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithUserHavingNoRoles_ReturnsEmptyRolesList()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == "user1")))
                    .ReturnsAsync(new List<string>());

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                UserManagementIndexViewModel? user1 = result.FirstOrDefault(u => u.Id == "user1");
                Assert.That(user1, Is.Not.Null);
                Assert.That(user1.Roles, Is.Not.Null);
                Assert.That(user1.Roles.Count(), Is.EqualTo(0));
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithOnlyCurrentUser_ReturnsEmptyList()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = new List<ApplicationUser>
                {
                    new ApplicationUser { Id = currentUserId, Email = "current@test.com" }
                };
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(0));
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithNoUsers_ReturnsEmptyList()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = new List<ApplicationUser>();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(0));
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithNullUserId_ThrowsNullReferenceException()
            {
                // Arrange
                string? currentUserId = null;
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);

                // Act & Assert
                NullReferenceException? exception = Assert.ThrowsAsync<NullReferenceException>(
                    async () => await userService.GetUsersManagementBoardDataAsync(currentUserId));

                Assert.That(exception, Is.Not.Null);
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithEmptyUserId_ReturnsAllUsers()
            {
                // Arrange
                string currentUserId = "";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(3)); // Debería incluir todos los usuarios
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithWhitespaceUserId_ReturnsAllUsers()
            {
                // Arrange
                string currentUserId = "   ";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(3)); // Debería incluir todos los usuarios (el filtro no excluye espacios en blanco)
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_ReturnsCorrectViewModelProperties()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                UserManagementIndexViewModel firstUser = result.First();

                Assert.That(firstUser, Is.InstanceOf<UserManagementIndexViewModel>());
                Assert.That(firstUser.Id, Is.Not.Null);
                Assert.That(firstUser.Email, Is.Not.Null);
                Assert.That(firstUser.Roles, Is.Not.Null);
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithExceptionInGetRoles_PropagatesException()
            {
                // Arrange
                string currentUserId = "current-user-id";
                List<ApplicationUser> users = CreateTestUsers();
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ThrowsAsync(new InvalidOperationException("Database error"));

                // Act & Assert
                InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(
                    async () => await userService.GetUsersManagementBoardDataAsync(currentUserId));

                Assert.That(exception.Message, Is.EqualTo("Database error"));
            }

            [Test]
            public async Task GetUsersManagementBoardDataAsync_WithEmptyUserId_ExcludesUserWithEmptyId()
            {
                // Arrange
                string currentUserId = "";
                List<ApplicationUser> users = new List<ApplicationUser>
                {
                    new ApplicationUser { Id = "", Email = "empty@test.com" },
                    new ApplicationUser { Id = "user1", Email = "user1@test.com" },
                    new ApplicationUser { Id = "user2", Email = "user2@test.com" }
                };
                IQueryable<ApplicationUser> mockQueryable = CreateMockQueryableWithUsers(users);

                this.mockUserManager.Setup(um => um.Users).Returns(mockQueryable);
                this.mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "User" });

                // Act
                IEnumerable<UserManagementIndexViewModel> result = await userService.GetUsersManagementBoardDataAsync(currentUserId);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.EqualTo(2)); // Debería excluir al usuario con ID vacío
                Assert.That(result.Any(u => u.Id == ""), Is.False);
            }

            private static List<ApplicationUser> CreateTestUsers()
            {
                return new List<ApplicationUser>
                {
                    new ApplicationUser { Id = "current-user-id", Email = "current@test.com" },
                    new ApplicationUser { Id = "user1", Email = "user1@test.com" },
                    new ApplicationUser { Id = "user2", Email = "user2@test.com" }
                };
            }

            private static IQueryable<ApplicationUser> CreateMockQueryableWithUsers(List<ApplicationUser> users)
            {
                return new TestAsyncEnumerable<ApplicationUser>(users);
            }
        }
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


