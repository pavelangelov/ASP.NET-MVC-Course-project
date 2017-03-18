using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LocationControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfLocationFactoryIsNull()
        {
            // Arrange
            var mockedLocationService = new Mock<ILocationService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LocationController(null, mockedLocationService.Object)).Message;
            StringAssert.Contains("locationFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLocationServiceIsNull()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LocationController(mockedLocationFactory.Object, null)).Message;
            StringAssert.Contains("locationService", message);
        }

        [Test]
        public void NotThrow_IfLocationFactoryAndLocationServiceAreValid()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLocationService = new Mock<ILocationService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new LocationController(mockedLocationFactory.Object, mockedLocationService.Object));
        }
    }
}
