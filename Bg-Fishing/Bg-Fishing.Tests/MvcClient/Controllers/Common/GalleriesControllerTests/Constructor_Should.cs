using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers.Common;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Common.GalleriesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfVideoServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new GalleriesController(null)).Message;
            StringAssert.Contains("videoService", message);
        }

        [Test]
        public void NotThrow_IfVideoServiceIsNotNull()
        {
            // Arrange
            var mockedService = new Mock<IVideoService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new GalleriesController(mockedService.Object));
        }
    }
}
