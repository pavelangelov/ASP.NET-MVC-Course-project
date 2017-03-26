using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Hubs;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Hubs.NewsHubTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfNewsServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new NewsHub(null)).Message;
            StringAssert.Contains("newsService", message);
        }

        [Test]
        public void NotThrow_IfNewsServiceIsNotNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new NewsHub(mockedNewsService.Object));
        }
    }
}
