using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers.Moderator;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Moderator.AddVideoControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfPassedVideoService_IsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new AddVideoController(null)).Message;
            StringAssert.Contains("videoService", message);
        }

        [Test]
        public void NotToThrow_IfVideoService_IsNotNull()
        {
            // Arrange
            var mockedService = new Mock<IVideoService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AddVideoController(mockedService.Object));
        }
    }
}
