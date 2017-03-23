using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.LakesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfLakeServiceIsNull()
        {
            // Arrange
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(null, mockedCommentFactory.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("lakeService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfCommentFactoryIsNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(mockedLakeService.Object, null, mockedDateProvider.Object)).Message;
            StringAssert.Contains("commentFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProviderIsNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(mockedLakeService.Object, mockedCommentFactory.Object, null)).Message;
            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new LakesController(mockedLakeService.Object, mockedCommentFactory.Object, mockedDateProvider.Object));
        }
    }
}
