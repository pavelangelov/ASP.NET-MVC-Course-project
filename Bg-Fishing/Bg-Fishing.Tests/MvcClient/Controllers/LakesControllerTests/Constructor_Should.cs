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
            var mockedCommentService = new Mock<ICommentService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(null, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("lakeService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfCommentServiceIsNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(mockedLakeService.Object, null, mockedCommentFactory.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("commentService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfCommentFactoryIsNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentService = new Mock<ICommentService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(mockedLakeService.Object, mockedCommentService.Object, null, mockedDateProvider.Object)).Message;
            StringAssert.Contains("commentFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProviderIsNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentService = new Mock<ICommentService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, null)).Message;
            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentService = new Mock<ICommentService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object));
        }
    }
}
