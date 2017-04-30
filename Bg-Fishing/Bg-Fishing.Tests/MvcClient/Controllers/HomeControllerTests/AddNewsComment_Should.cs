using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Comments;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.HomeControllerTests
{
    [TestFixture]
    public class AddNewsComment_Should
    {
        [Test]
        public void RemoveSuccessMessage_FromTempData_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedNews = new News();
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.FindById(It.IsAny<string>())).Returns(mockedNews).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Verifiable();

            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            mockedNewsCommentFactory.Setup(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var controller = new HomeController(mockedNewsService.Object, mockedNewsCommentFactory.Object, mockedDateProvider.Object);
            var model = new NewsDetailsViewModel() { NewsId = mockedNews.Id };
            controller.ModelState.AddModelError("Test error", "test message");

            // Act
            var result = controller.AddNewsComment(model) as ViewResult;
            var viewModel = result.ViewData.Model as NewsDetailsViewModel;

            // Assert
            Assert.AreEqual("News", result.ViewName);
            Assert.AreEqual(model, viewModel);
            Assert.IsNull(result.TempData["AddCommentSuccess"]);

            mockedNewsService.Verify(s => s.FindById(model.NewsId), Times.Once);
            mockedNewsService.Verify(s => s.Save(), Times.Never);

            mockedNewsCommentFactory.Verify(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Never);
        }

        [Test]
        public void SetErrorMessage_InModelState_IfAddingCommentFailed()
        {
            // Arrange
            var mockedNews = new News();
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.FindById(It.IsAny<string>())).Returns(mockedNews).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Verifiable();

            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            mockedNewsCommentFactory.Setup(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Throws<Exception>();

            var controller = new HomeController(mockedNewsService.Object, mockedNewsCommentFactory.Object, mockedDateProvider.Object);
            var model = new NewsDetailsViewModel() { NewsId = mockedNews.Id };

            // Act
            var result = controller.AddNewsComment(model) as ViewResult;
            var viewModel = result.ViewData.Model as NewsDetailsViewModel;

            // Assert
            Assert.AreEqual("News", result.ViewName);
            Assert.AreEqual(model, viewModel);
            Assert.IsNull(result.TempData["AddCommentSuccess"]);
            Assert.IsTrue(result.ViewData.ModelState.ContainsKey(""));

            mockedNewsService.Verify(s => s.FindById(model.NewsId), Times.Once);
            mockedNewsService.Verify(s => s.Save(), Times.Never);

            mockedNewsCommentFactory.Verify(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Test]
        public void AddCommentToNews_AndRedirectToNews_IfAddingCommentNotFailed()
        {
            // Arrange
            var mockedNews = new News();
            var mockedNewsComment = new NewsComment();
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.FindById(It.IsAny<string>())).Returns(mockedNews).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Verifiable();

            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            mockedNewsCommentFactory.Setup(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(mockedNewsComment).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedContext = new Mock<ControllerContext>();
            mockedContext.Setup(c => c.HttpContext.User.Identity.Name).Returns("test");

            var controller = new HomeController(mockedNewsService.Object, mockedNewsCommentFactory.Object, mockedDateProvider.Object);
            controller.ControllerContext = mockedContext.Object;
            var model = new NewsDetailsViewModel() { NewsId = mockedNews.Id };

            // Act
            var result = controller.AddNewsComment(model) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues.ContainsKey("newsId"));
            Assert.AreEqual(mockedNews.Id, result.RouteValues["newsId"]);
            Assert.AreEqual("News", result.RouteValues["action"]);
            Assert.IsTrue(mockedNews.Comments.Count == 1);
            Assert.IsTrue(mockedNews.Comments.Contains(mockedNewsComment));

            mockedNewsService.Verify(s => s.FindById(model.NewsId), Times.Once);
            mockedNewsService.Verify(s => s.Save(), Times.Once);

            mockedNewsCommentFactory.Verify(f => f.CreateNewsComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
