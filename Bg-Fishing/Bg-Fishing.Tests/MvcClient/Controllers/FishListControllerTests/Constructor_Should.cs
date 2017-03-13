using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.FishListControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfFishServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new FishListController(null)).Message;
            StringAssert.Contains("fishService", message);
        }

        [Test]
        public void NotThrow_IfFishServiceIsNotNull()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new FishListController(mockedFishService.Object));
        }
    }
}
