using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfNewsServiceIsNull()
        {
            // Arrange
            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new HomeController(
                null, 
                mockedNewsCommentFactory.Object, 
                mockedDateProvider.Object)).Message;

            StringAssert.Contains("newsService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfNewsCommentFactoryIsNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new HomeController(
                mockedNewsService.Object,
                null, 
                mockedDateProvider.Object)).Message;

            StringAssert.Contains("newsCommentFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProviderIsNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new HomeController(
                mockedNewsService.Object,
                mockedNewsCommentFactory.Object,
                null)).Message;

            StringAssert.Contains("dateProvider", message);
        }


        [Test]
        public void NotThrow_IfAllDependenciesAreValid()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HomeController(mockedNewsService.Object, mockedNewsCommentFactory.Object, mockedDateProvider.Object));
        }
    }
}
