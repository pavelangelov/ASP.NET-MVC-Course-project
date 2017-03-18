using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LocationServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void AddLocationToContext()
        {
            // Arrange
            var mockedCollection = new List<Location>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<Location>())).Callback<Location>((location) => mockedCollection.Add(location));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Locations).Returns(mockedDbSet.Object);

            var locationService = new LocationService(mockedDbContext.Object);
            var locationToAdd = new Location(2.1, 2.1, "Test location");

            // Act
            locationService.Add(locationToAdd);

            // Assert
            Assert.IsTrue(mockedCollection.Count == 1);
            Assert.AreEqual(locationToAdd, mockedCollection[0]);
        }
    }
}
