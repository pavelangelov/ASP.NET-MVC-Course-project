﻿using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class FindByName_Should
    {
        [Test]
        public void ReturnCorrectResult_IfNameMatch()
        {
            // Arrange
            var fishCollection = Utils.GetFishCollection();
            var mockedDbSet = MockDbSet.Mock(fishCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);
            var searchedFish = fishCollection.Last();

            // Act
            var fish = fishService.FindByName(searchedFish.Name);

            // Assert
            Assert.AreEqual(searchedFish.Name, fish.Name);
            Assert.AreEqual(searchedFish.Id, fish.Id);
        }

        [Test]
        public void ReturnNull_IfNameNotMatch()
        {
            // Arrange
            var fishCollection = Utils.GetFishCollection();
            var mockedDbSet = MockDbSet.Mock(fishCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Fish).Returns(mockedDbSet.Object);

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            var fish = fishService.FindByName("Invalid name");

            // Assert
            Assert.IsNull(fish);
        }
    }
}
