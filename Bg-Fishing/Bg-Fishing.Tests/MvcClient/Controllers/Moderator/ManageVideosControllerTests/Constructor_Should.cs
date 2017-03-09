using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers.Moderator;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Moderator.ManageVideosControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfPassedVideoService_IsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new ManageVideosController(null)).Message;
            StringAssert.Contains("videoService", message);
        }

        [Test]
        public void NotToThrow_IfVideoService_IsNotNull()
        {
            // Arrange
            var mockedService = new Mock<IVideoService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new ManageVideosController(mockedService.Object));
        }
    }
}
