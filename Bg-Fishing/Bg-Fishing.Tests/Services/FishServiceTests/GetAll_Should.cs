using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnCorrectResult_IfCollectionIsNotEmpty()
        {
            // Arrange
            var fishCollection = Utils.GetFishCollection();
            var mockedDbSet = MockDbSet.Mock(fishCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            var allFish = fishService.GetAll();

            // Assert
            Assert.IsTrue(allFish.Count() == fishCollection.Count);
            var index = 0;
            foreach (var fish in allFish)
            {
                Assert.AreEqual(fishCollection[index].Name, fish.Name);
                Assert.AreEqual(fishCollection[index].ImageUrl, fish.ImageUrl);
                index++;
            }
        }

        [Test]
        public void ReturnNull_IfCollectionIsEmpty()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Verifiable();

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            var fish = fishService.GetAll();

            // Assert
            Assert.IsNull(fish);
            mockedDbContext.Verify(c => c.Fish, Times.Once);
        }
    }
}
