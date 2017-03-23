using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.CommentsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfCommentServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new CommentsController(null)).Message;
            StringAssert.Contains("commentService", message);
        }

        [Test]
        public void NotThrow_IfCommentServiceIsNotNull()
        {
            // Arrange
            var mockedCommentService = new Mock<ICommentService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new CommentsController(mockedCommentService.Object));
        }
    }
}
