using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class FindByLocation_Should
    {
        [Test]
        public void ReturnCorrectResult_IfLocationMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            var result = lakeService.FindByLocation("Valid");

            // Assert
            Assert.IsTrue(result.Count() == 2);
            Assert.AreEqual(mockedCollection.First().Name, result.First().Name);
            Assert.AreEqual(mockedCollection.Last().Name, result.Last().Name);
        }

        [Test]
        public void ReturnCorrectResult_IfLocationNotMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            var result = lakeService.FindByLocation("Not Valid");

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
