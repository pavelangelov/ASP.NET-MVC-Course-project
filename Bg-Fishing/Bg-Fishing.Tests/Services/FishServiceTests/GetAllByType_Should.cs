using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class GetAllByType_Should
    {
        [Test]
        public void ReturnCorrectResult_IfCollectionContainsFish_AndTypeMatch()
        {
            // Arrange
            var allFish = this.GetFish();
            var mockedDbSet = MockDbSet.Mock(allFish.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            var fishByType = fishService.GetAllByType(FishType.FreshAndSaltWather);

            // Assert
            Assert.IsTrue(fishByType.Count() == 2);
            Assert.AreEqual(allFish.First().Name, fishByType.First().Name);
            Assert.AreEqual(allFish.Last().Name, fishByType.Last().Name);
        }

        [Test]
        public void ReturnEmptyCollection_IfCollectionContainsFish_AndTypeNotMatch()
        {
            // Arrange
            var allFish = this.GetFish();
            var mockedDbSet = MockDbSet.Mock(allFish.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            var fishByType = fishService.GetAllByType(FishType.Saltwater);

            // Assert
            Assert.IsTrue(fishByType.Count() == 0);
        }

        public ICollection<Fish> GetFish()
        {
            return new List<Fish>
            {
                new Fish("First", FishType.FreshAndSaltWather, "fish url"),
                new Fish("First", FishType.SeaFish, "fish url"),
                new Fish("First", FishType.FreshAndSaltWather, "fish url")
            };
        }
    }
}
