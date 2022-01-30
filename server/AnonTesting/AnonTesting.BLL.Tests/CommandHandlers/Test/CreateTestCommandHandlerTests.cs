using AnonTesting.BLL.CommandHandlers.Test;
using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Test
{
    [TestClass]
    public class CreateTestCommandHandlerTests
    {
        [TestMethod]
        public async Task Handle_VaildCommandToCreateTest_ShouldMapAndSaveInDbContext()
        {
            //arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<TestDto, DAL.Model.Test>(It.IsAny<TestDto>())).Returns(new DAL.Model.Test()
            {
                Id = Guid.NewGuid()
            });

            var dbSetMock = new Mock<DbSet<DAL.Model.Test>>();
            dbSetMock.Setup(s => s.Add(It.IsAny<DAL.Model.Test>()));

            var contextMock = new Mock<ApplicationContext>();
            contextMock.Setup(c => c.Tests).Returns(dbSetMock.Object);
            contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var sut = new CreateTestCommandHandler(contextMock.Object, mapperMock.Object);

            //act
            var createdId = await sut.Handle(new CreateTestCommand(new TestDto()), default);

            //assert
            Assert.AreNotEqual(Guid.Empty, createdId);
            dbSetMock.Verify(s => s.Add(It.IsAny<DAL.Model.Test>()), Times.Once);
            contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
