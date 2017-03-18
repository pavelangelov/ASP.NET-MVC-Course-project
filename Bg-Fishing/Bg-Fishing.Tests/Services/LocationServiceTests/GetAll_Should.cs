using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;
using Bg_Fishing.Models;

namespace Bg_Fishing.Tests.Services.LocationServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnCorrectResult_IfCollectionIsNotEmpty()
        {
            // Arrange
            var mockedCollection = Utils.GetLocationsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Locations).Returns(mockedDbSet.Object);

            var locationService = new LocationService(mockedDbContext.Object);

            // Act
            var result = locationService.GetAll();

            // Assert
            Assert.IsTrue(result.Count() == 3);
            var index = 0;
            foreach (var location in result)
            {
                Assert.AreEqual(mockedCollection[index].Name, location.Name);
                index++;
            }
        }

        [Test]
        public void ReturnCorrectResult_IfCollectionIsEmpty()
        {
            // Arrange
            var mockedCollection = new List<Location>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Locations).Returns(mockedDbSet.Object);

            var locationService = new LocationService(mockedDbContext.Object);

            // Act
            var result = locationService.GetAll();

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
