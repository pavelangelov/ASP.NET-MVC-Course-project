using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.CommentsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfCommentServiceIsNull()
        {
            // Arrange
            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new CommentsController(
                null, 
                mockedInnerCommentFactory.Object, 
                mockedDateProvider.Object)).Message;

            StringAssert.Contains("commentService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfInnerCommentFactoryIsNull()
        {
            // Arrange
            var mockedCommentService = new Mock<ICommentService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new CommentsController(
                mockedCommentService.Object,
                null,
                mockedDateProvider.Object)).Message;

            StringAssert.Contains("innerCommentFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProviderIsNull()
        {
            // Arrange
            var mockedCommentService = new Mock<ICommentService>();
            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new CommentsController(
                mockedCommentService.Object,
                mockedInnerCommentFactory.Object,
                null)).Message;

            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void NotThrow_IfAllDependenciesAreValid()
        {
            // Arrange
            var mockedCommentService = new Mock<ICommentService>();
            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            // Act & Assert
            Assert.DoesNotThrow(() => new CommentsController(mockedCommentService.Object, mockedInnerCommentFactory.Object, mockedDateProvider.Object));
        }
    }
}
