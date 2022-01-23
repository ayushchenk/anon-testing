using AnonTesting.BLL.Model;
using AnonTesting.BLL.Services;
using AnonTesting.BLL.Services.Abstract;
using AnonTesting.DAL.Interfaces;
using AnonTesting.DAL.Model;
using AutoFixture;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.Services
{
    [TestClass]
    public class GenericServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IEntityRepository<Test>> _repositoryMock = new Mock<IEntityRepository<Test>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private GenericService<Test, TestDto> _sut = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _repositoryMock.Reset();

            _mapperMock.Reset();
            _mapperMock.Setup(m => m.Map<TestDto, Test>(It.IsAny<TestDto>())).Returns(new Test());
            _mapperMock.Setup(m => m.Map<Test, TestDto>(It.IsAny<Test>())).Returns(new TestDto());

            _sut = new TestService(_repositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task CreateAsync_NewTestRecord_ShouldCallDependenciesOnce()
        {
            //arrange
            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Test>())).ReturnsAsync(Guid.NewGuid());

            var newRecord = _fixture.Create<TestDto>();

            //act
            var newId = await _sut.CreateAsync(newRecord);

            //assert
            Assert.AreNotEqual(Guid.Empty, newId);
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Test>()), Times.Once);
            _mapperMock.Verify(m => m.Map<TestDto, Test>(It.IsAny<TestDto>()), Times.Once);
        }
        
        [TestMethod]
        [DataRow(5)]
        [DataRow(25)]
        [DataRow(125)]
        public async Task BulkCreateAsync_SeveralTestRecords_ShouldCallDependenciesExactlyNTimes(int recordsCount)
        {
            //arrange
            _repositoryMock.Setup(r => r.BulkCreateAsync(It.IsAny<IEnumerable<Test>>())).Returns<IEnumerable<Test>>(input =>
            {
                input.ToList();
                return ValueTask.CompletedTask;
            });

            var newRecords = _fixture.CreateMany<TestDto>(recordsCount);

            //act
            await _sut.BulkCreateAsync(newRecords);

            //assert
            _repositoryMock.Verify(r => r.BulkCreateAsync(It.IsAny<IEnumerable<Test>>()), Times.Once);
            _mapperMock.Verify(m => m.Map<TestDto, Test>(It.IsAny<TestDto>()), Times.Exactly(recordsCount));
        }

        [TestMethod]
        public async Task GetAsync_ExistingRecord_ShouldMapAndReturn()
        {
            //arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new Test());

            //act
            var queriedEntity = await _sut.GetAsync(Guid.NewGuid());

            //assert
            Assert.IsNotNull(queriedEntity);
            _repositoryMock.Verify(r => r.GetAsync(It.IsAny<Guid>()), Times.Once);
            _mapperMock.Verify(m => m.Map<Test, TestDto>(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_NotExistingRecord_ShouldReturnNull()
        {
            //arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

            //act
            var queriedEntity = await _sut.GetAsync(Guid.NewGuid());

            //assert
            Assert.IsNull(queriedEntity);
            _repositoryMock.Verify(r => r.GetAsync(It.IsAny<Guid>()), Times.Once);
            _mapperMock.Verify(m => m.Map<Test, TestDto>(It.IsAny<Test>()), Times.Never);
        }

        [TestMethod]
        [DataRow(5)]
        [DataRow(25)]
        [DataRow(125)]
        public async Task GetAllAsync_SeveralTestRecords_ShouldCallDependenciesExactlyNTimes(int recordsCount)
        {
            //arrange
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(Enumerable.Range(0, recordsCount).Select(i => new Test()));

            //act
            var queriedRecords = await _sut.GetAllAsync();

            //assert
            Assert.AreEqual(recordsCount, queriedRecords.Count());
            _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<Test, TestDto>(It.IsAny<Test>()), Times.Exactly(recordsCount));
        }
    }
}
