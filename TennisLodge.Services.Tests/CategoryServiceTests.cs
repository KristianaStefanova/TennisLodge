using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TennisLodge.Data.Models;
using TennisLodge.Data.Repository.Interfaces;
using TennisLodge.Services.Core;
using TennisLodge.Web.ViewModels.Tournament;
using TennisLodge.Data;

namespace TennisLodge.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> mockCategoryRepository;
        private CategoryService categoryService;
        private TennisLodgeDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            mockCategoryRepository = new Mock<ICategoryRepository>();
            categoryService = new CategoryService(mockCategoryRepository.Object);

            DbContextOptions<TennisLodgeDbContext> options = new DbContextOptionsBuilder<TennisLodgeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            dbContext = new TennisLodgeDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext?.Dispose();
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenCategoriesExist_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Singles" },
                new Category { Id = 2, Name = "Doubles" },
                new Category { Id = 3, Name = "Mixed Doubles" }
            };

            // Add categories to in-memory database
            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();

            // Setup mock to return the actual DbSet from in-memory database
            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Returns(dbContext.Categories.AsQueryable());

            // Act
            IEnumerable<CategoryViewModel> result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<CategoryViewModel>>());
            
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(3));
            
            Assert.Multiple(() =>
            {
                Assert.That(resultList[0].Id, Is.EqualTo(1));
                Assert.That(resultList[0].Name, Is.EqualTo("Singles"));
                
                Assert.That(resultList[1].Id, Is.EqualTo(2));
                Assert.That(resultList[1].Name, Is.EqualTo("Doubles"));
                
                Assert.That(resultList[2].Id, Is.EqualTo(3));
                Assert.That(resultList[2].Name, Is.EqualTo("Mixed Doubles"));
            });

            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenNoCategoriesExist_ReturnsEmptyCollection()
        {
            // Arrange - No categories in database
            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Returns(dbContext.Categories.AsQueryable());

            // Act
            IEnumerable<CategoryViewModel> result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<CategoryViewModel>>());
            Assert.That(result, Is.Empty);

            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenSingleCategoryExists_ReturnsSingleCategory()
        {
            // Arrange
            Category category = new Category { Id = 1, Name = "Singles" };

            // Add category to in-memory database
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Returns(dbContext.Categories.AsQueryable());

            // Act
            IEnumerable<CategoryViewModel> result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<CategoryViewModel>>());

            List<CategoryViewModel> resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].Name, Is.EqualTo("Singles"));

            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenCategoriesHaveEmptyNames_ReturnsCategoriesWithEmptyNames()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "" },
                new Category { Id = 2, Name = "Doubles" }
            };

            // Add categories to in-memory database
            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();

            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Returns(dbContext.Categories.AsQueryable());

            // Act
            IEnumerable<CategoryViewModel> result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            List<CategoryViewModel> resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList[0].Name, Is.EqualTo(""));
            Assert.That(resultList[1].Name, Is.EqualTo("Doubles"));

            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenRepositoryThrowsException_PropagatesException()
        {
            // Arrange
            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Throws(new InvalidOperationException("Database connection failed"));

            // Act & Assert
            InvalidOperationException? exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await categoryService.GetAllCategoriesAsync());

            Assert.That(exception.Message, Is.EqualTo("Database connection failed"));
            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }

        [Test]
        public void CategoryService_Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryService(null!));
        }

        [Test]
        public async Task GetAllCategoriesAsync_WhenCategoriesHaveSpecialCharacters_ReturnsCategoriesWithSpecialCharacters()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Men's Singles" },
                new Category { Id = 2, Name = "Women's Doubles" },
                new Category { Id = 3, Name = "Mixed & Doubles" }
            };

            // Add categories to in-memory database
            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();

            mockCategoryRepository
                .Setup(x => x.GetAllAttached())
                .Returns(dbContext.Categories.AsQueryable());

            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            var resultList = result.ToList();
            Assert.That(resultList.Count, Is.EqualTo(3));
            
            Assert.Multiple(() =>
            {
                Assert.That(resultList[0].Name, Is.EqualTo("Men's Singles"));
                Assert.That(resultList[1].Name, Is.EqualTo("Women's Doubles"));
                Assert.That(resultList[2].Name, Is.EqualTo("Mixed & Doubles"));
            });

            mockCategoryRepository.Verify(x => x.GetAllAttached(), Times.Once);
        }
    }
}
