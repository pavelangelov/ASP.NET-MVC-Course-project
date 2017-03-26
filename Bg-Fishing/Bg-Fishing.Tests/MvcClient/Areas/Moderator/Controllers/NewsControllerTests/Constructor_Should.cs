using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.NewsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfNewsFactoryIsNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new NewsController(null, mockedNewsService.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("newsFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfNewsServiceIsNull()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new NewsController(mockedNewsFactory.Object, null, mockedDateProvider.Object)).Message;
            StringAssert.Contains("newsService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProviderIsNull()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedNewsService = new Mock<INewsService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new NewsController(mockedNewsFactory.Object, mockedNewsService.Object, null)).Message;
            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new NewsController(mockedNewsFactory.Object, mockedNewsService.Object, mockedDateProvider.Object));
        }
    }
}
