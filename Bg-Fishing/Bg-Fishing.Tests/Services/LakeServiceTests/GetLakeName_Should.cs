using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class GetLakeName_Should
    {
        [Test]
        public void ReturnCorrectValue_IfIdMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);
            var searchedLake = mockedCollection[1];

            // Act
            var result = lakeService.GetLakeName(searchedLake.Id);

            // Assert
            Assert.AreEqual(searchedLake.Name, result);
        }
    }
}
