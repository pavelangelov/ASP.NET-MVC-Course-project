using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void AddFishToDbContext()
        {
            // Arrange
            var mockedCollection = new List<Fish>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<Fish>())).Callback<Fish>(f => mockedCollection.Add(f));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);
            var fish = new Fish();

            // Act
            fishService.Add(fish);

            // Assert
            Assert.IsTrue(mockedCollection.Count == 1);
            Assert.IsTrue(mockedCollection.Contains(fish));
        }
    }
}
