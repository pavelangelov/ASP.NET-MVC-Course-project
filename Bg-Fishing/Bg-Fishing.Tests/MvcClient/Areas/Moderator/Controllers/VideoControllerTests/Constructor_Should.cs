using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfPassedVideoService_IsNull()
        {
            // Arrange
            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new VideoController(null, mockedVideoFactory.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("videoService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfPassedVideoFactory_IsNull()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new VideoController(mockedVideoService.Object, null, mockedDateProvider.Object)).Message;
            StringAssert.Contains("videoFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfPassedDateProvider_IsNull()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            var mockedVideoFactory = new Mock<IVideoFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, null)).Message;
            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void NotToThrow_IfVideoService_IsNotNull()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, mockedDateProvider.Object));
        }
    }
}
