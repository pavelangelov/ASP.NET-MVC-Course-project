using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LocationServiceTests
{
    [TestFixture]
    public class FindByName_Should
    {
        [Test]
        public void ReturnCorrectResult_IfNameMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLocationsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Locations).Returns(mockedDbSet.Object);

            var locationService = new LocationService(mockedDbContext.Object);
            var expectedLocation = mockedCollection[1];

            // Act
            var result = locationService.FindByName(expectedLocation.Name);

            // Assert
            Assert.AreEqual(expectedLocation.Name, result.Name);
            Assert.AreEqual(expectedLocation.Latitude, result.Latitude);
            Assert.AreEqual(expectedLocation.Longitude, result.Longitude);
        }

        [Test]
        public void ReturnNull_IfNameNotMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetLocationsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Locations).Returns(mockedDbSet.Object);

            var locationService = new LocationService(mockedDbContext.Object);
            var expectedLocation = mockedCollection[1];

            // Act
            var result = locationService.FindByName("Invalid name");

            // Assert
            Assert.IsNull(result);
        }
    }
}
