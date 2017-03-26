using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfNewsServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new HomeController(null)).Message;
            StringAssert.Contains("newsService", message);
        }

        [Test]
        public void NotThrow_IfNewsServiceIsNotNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HomeController(mockedNewsService.Object));
        }
    }
}
