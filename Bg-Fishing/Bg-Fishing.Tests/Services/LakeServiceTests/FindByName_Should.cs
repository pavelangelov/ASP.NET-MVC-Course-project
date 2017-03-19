using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class FindByName_Should
    {
        [Test]
        public void ReturnCorrectResult_IfNameMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);
            var searchedLake = mockedCollection[1];

            // Act
            var result = lakeService.FindByName(searchedLake.Name);

            // Assert
            Assert.AreEqual(searchedLake, result);
        }

        [Test]
        public void ReturnCorrectResult_IfNameNotMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            var result = lakeService.FindByName("Invalid name");

            // Assert
            Assert.IsNull(result);
        }
    }
}
