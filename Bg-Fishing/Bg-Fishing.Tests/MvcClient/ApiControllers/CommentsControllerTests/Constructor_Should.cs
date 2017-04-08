using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.CommentsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfCommentServiceIsNull()
        {
            // Arrange, Act & Assert
            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var message = Assert.Throws<ArgumentNullException>(() => new CommentsController(null, mockedInnerCommentFactory.Object, mockedDateProvider.Object)).Message;
            StringAssert.Contains("commentService", message);
        }

        [Test]
        public void NotThrow_IfCommentServiceIsNotNull()
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
