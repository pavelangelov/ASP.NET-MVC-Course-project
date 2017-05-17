using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.FishControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfFishFactoryIsNull()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new FishController(null, mockedFishService.Object)).Message;
            StringAssert.Contains("fishFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfFishServiceIsNull()
        {
            // Arrange
            var mockedFishFactory = new Mock<IFishFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new FishController(mockedFishFactory.Object, null)).Message;

            StringAssert.Contains("fishService", message);
        }

        [Test]
        public void NotThrow_IfDependenciesAreValid()
        {
            // Arrange
            var mockedFishFactory = new Mock<IFishFactory>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new FishController(mockedFishFactory.Object, mockedFishService.Object));
        }
    }
}
