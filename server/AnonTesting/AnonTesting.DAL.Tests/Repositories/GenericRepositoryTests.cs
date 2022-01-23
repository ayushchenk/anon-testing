using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AnonTesting.DAL.Tests.Repositories
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private ApplicationContext _context = null!;
        private GenericRepository<Test> _repository = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("_AnonTesting")
                .Options;

            _context = new ApplicationContext(contextOptions);
            _repository = new TestRepository(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public async Task CreateAsync_TestEntity_ShouldReturnNonEmptyId()
        {
            //arrange
            var test = new Test()
            {
                Title = "test"
            };

            //act
            await _repository.CreateAsync(test);

            //
            Assert.AreNotEqual(Guid.Empty, test.Id);
        }

        [TestMethod]
        public async Task GetAsync_TestEntityAfterCreate_ShouldReturnEntity()
        {
            //arrange
            var sourceEntity = new Test()
            {
                Title = "test"
            };

            await _repository.CreateAsync(sourceEntity);

            //act
            var queriedEntity = await _repository.GetAsync(sourceEntity.Id);

            //assert
            Assert.AreEqual(sourceEntity.Title, queriedEntity?.Title);
        }

        [TestMethod]
        public async Task GetAsync_ByRandomGuid_ShouldReturnNull()
        {
            //arrange
            var randomGuid = Guid.NewGuid();

            //act
            var queriedEntity = await _repository.GetAsync(randomGuid);

            //assert
            Assert.IsNull(queriedEntity);
        }

        [TestMethod]
        public async Task GetAllAsync_TestEntitiesAfterCreate_ShouldReturnEntities()
        {
            //arrange
            var sourceEntities = new Test[]
            {
                new Test(){ Title = "test1"},
                new Test(){ Title = "test2"},
                new Test(){ Title = "test3"},
            };

            await _repository.BulkCreateAsync(sourceEntities);

            //act
            var queriedEntities = await _repository.GetAllAsync();

            //assert
            foreach(var sourceEntity in sourceEntities)
            {
                Assert.AreEqual(sourceEntity.Title, queriedEntities.First(e => e.Id == sourceEntity.Id).Title);
            }
        }

        [TestMethod] 
        public async Task FindByAsync_TitleEndingWithZero_ShouldReturnOneEntity()
        {
            //arrange
            var sourceEntities = new Test[]
            {
                new Test(){ Title = "test0"},
                new Test(){ Title = "test1"},
                new Test(){ Title = "test2"},
            };

            await _repository.BulkCreateAsync(sourceEntities);

            //act
            var queriedEntities = await _repository.FindByAsync(e => e.Title.EndsWith("0"));

            //assert
            Assert.AreEqual(1, queriedEntities.Count());
            Assert.AreEqual("test0", queriedEntities.First().Title);
        }

        [TestMethod]
        public async Task UpdateAsync_CreatedTestEntity_ShouldUpdateTitle()
        {
            //arrange
            var sourceEntity = new Test()
            {
                Title = "initial"
            };

            await _repository.CreateAsync(sourceEntity);

            //act
            var updateEntity = new Test()
            {
                Id = sourceEntity.Id,
                Title = "updated"
            };

            await _repository.UpdateAsync(updateEntity);

            var queriedEntity = await _repository.GetAsync(sourceEntity.Id);

            //assert
            Assert.AreEqual(updateEntity.Title, queriedEntity?.Title);
        }

        [TestMethod]
        public async Task DeleteAsync_CreatedTestEntity_ShouldReturnTrue()
        {
            //arrange
            var sourceEntity = new Test()
            {
                Title = "test"
            };

            await _repository.CreateAsync(sourceEntity);

            //act
            var isDeleted = await _repository.DeleteAsync(sourceEntity.Id);

            var queriedEntity = await _repository.GetAsync(sourceEntity.Id);

            //assert
            Assert.IsTrue(isDeleted);
            Assert.IsNull(queriedEntity);
        }

        [TestMethod]
        public async Task DeleteAsync_NoCreatedEntities_ShouldReturnFalse()
        {
            //arrange
            var randomGuid = Guid.NewGuid();

            //act
            var isDeleted = await _repository.DeleteAsync(randomGuid);

            //assert
            Assert.IsFalse(isDeleted);
        }
    }
}
