using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models.Comments;
using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.CommentsControllerTests
{
    [TestFixture]
    public class Add_Should
    {
        public readonly string ExpectedErrorMessage = "error: Коментарът не може да бъде добавен!";

        [Test]
        public void CorrectErroMessage_IfCreatingCommentFailed()
        {
            // Arrange

            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.FindById(It.IsAny<string>())).Verifiable();
            mockedCommentService.Setup(s => s.Save()).Verifiable();

            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            mockedInnerCommentFactory.Setup(f => f.CreateInnerComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Throws<Exception>();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var controller = new CommentsController(
                mockedCommentService.Object,
                mockedInnerCommentFactory.Object,
                mockedDateProvider.Object);

            // Act
            var result = controller.Add(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(ExpectedErrorMessage, result);

            mockedCommentService.Verify(s => s.FindById(It.IsAny<string>()), Times.Never);
            mockedCommentService.Verify(s => s.Save(), Times.Never);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);
        }

        [Test]
        public void CorrectErrorMessage_IfSavingChangesFailed()
        {
            // Arrange
            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.FindById(It.IsAny<string>())).Verifiable();
            mockedCommentService.Setup(s => s.Save()).Throws<Exception>();

            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            mockedInnerCommentFactory.Setup(f => f.CreateInnerComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var controller = new CommentsController(
                mockedCommentService.Object,
                mockedInnerCommentFactory.Object,
                mockedDateProvider.Object);

            // Act
            var result = controller.Add(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual(ExpectedErrorMessage, result);

            mockedCommentService.Verify(s => s.FindById(It.IsAny<string>()), Times.Once);

            mockedInnerCommentFactory.Verify(f => f.CreateInnerComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);
        }

        [Test]
        public void CorrectMessage_IfNothingFailed()
        {
            // Arrange
            var mockedComment = new Comment();
            var mockedInnerComment = new InnerComment();

            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.FindById(It.IsAny<string>())).Returns(mockedComment).Verifiable();
            mockedCommentService.Setup(s => s.Save()).Verifiable();

            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            mockedInnerCommentFactory.Setup(f => f.CreateInnerComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(mockedInnerComment).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var controller = new CommentsController(
                mockedCommentService.Object,
                mockedInnerCommentFactory.Object,
                mockedDateProvider.Object);

            // Act
            var result = controller.Add(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.AreEqual("success", result);
            Assert.IsTrue(mockedComment.Comments.Contains(mockedInnerComment));

            mockedCommentService.Verify(s => s.FindById(It.IsAny<string>()), Times.Once);
            mockedCommentService.Verify(s => s.Save(), Times.Once);

            mockedInnerCommentFactory.Verify(f => f.CreateInnerComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);
        }
    }
}
