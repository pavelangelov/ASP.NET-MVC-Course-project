using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.VideosApiControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfVideoServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new VideosApiController(null)).Message;
            StringAssert.Contains("videoService", message);
        }

        [Test]
        public void NotThrow_IfVideoServiceIsNotNull()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new VideosApiController(mockedVideoService.Object));
        }
    }
}
