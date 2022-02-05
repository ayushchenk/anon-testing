using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using AnonTesting.BLL.QueryHandlers.Test;
using AnonTesting.DAL.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.QueryHandlers.Test
{
    [TestClass]
    public class GetTestByIdQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<DbSet<DAL.Model.Test>> _dbSetMock;
        private readonly Mock<ApplicationContext> _contextMock;
        private readonly GetTestByIdQueryHandler _sut;

        public GetTestByIdQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.Map<DAL.Model.Test, TestDto>(It.IsAny<DAL.Model.Test>()))
                .Returns(new TestDto());

            _dbSetMock = new Mock<DbSet<DAL.Model.Test>>();

            _contextMock = new Mock<ApplicationContext>();
            _contextMock.Setup(c => c.Tests).Returns(_dbSetMock.Object);

            _sut = new GetTestByIdQueryHandler(_contextMock.Object, _mapperMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _dbSetMock.Reset();
        }

        [TestMethod]
        public async Task Handle_QueryToExistingTest_ShouldMapEndReturn()
        {
            //arrange
            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new DAL.Model.Test());

            var query = new GetTestByIdQuery(Guid.NewGuid());

            //act
            var testDto = await _sut.Handle(query, default);

            //assert
            Assert.IsNotNull(testDto);
            _mapperMock.Verify(m => m.Map<DAL.Model.Test, TestDto>(It.IsAny<DAL.Model.Test>()), Times.Once);
        }

        [TestMethod]
        public async Task Handle_QueryToNotExistingTest_ShouldNotMapEndReturnNull()
        {
            //arrange
            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(value: null);

            var query = new GetTestByIdQuery(Guid.NewGuid());

            //act
            var testDto = await _sut.Handle(query, default);

            //assert
            Assert.IsNull(testDto);
            _mapperMock.Verify(m => m.Map<DAL.Model.Test, TestDto>(It.IsAny<DAL.Model.Test>()), Times.Never);
        }
    }
}
