using System;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Tests.MvcClient.Mocks;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.NewsControllerTests
{
    [TestFixture]
    public class PostIndex_Should
    {
        [Test]
        public void ReturnViewWithModelErrors_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            mockedNewsFactory.Setup(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.Add(It.IsAny<News>())).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var controller = new NewsController(mockedNewsFactory.Object, mockedNewsService.Object, mockedDateProvider.Object);

            var expextedError = "test error";
            var expectedErrorMessage = "Test";
            controller.ModelState.AddModelError(expextedError, expectedErrorMessage);

            var model = new AddNewsViewModel();
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Index(model, mockedFile) as ViewResult;

            // Assert
            ModelState modelError;
            result.ViewData.ModelState.TryGetValue(expextedError, out modelError);

            Assert.AreEqual("", result.ViewName);
            Assert.IsTrue(modelError.Errors.First().ErrorMessage == expectedErrorMessage);
            Assert.IsTrue(result.TempData[GlobalMessages.AddNewsSuccessKey] == null);

            mockedNewsFactory.Verify(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            mockedNewsService.Verify(s => s.Add(It.IsAny<News>()), Times.Never);
            mockedNewsService.Verify(s => s.Save(), Times.Never);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Never);
        }

        [Test]
        public void ReturnViewWithModelErrors_IfAddindNewsFailed()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            mockedNewsFactory.Setup(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.Add(It.IsAny<News>())).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Throws(new Exception("Test"));

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");

            var controller = new NewsController(mockedNewsFactory.Object, mockedNewsService.Object, mockedDateProvider.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddNewsViewModel();
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Index(model, mockedFile) as ViewResult;

            // Assert
            ModelState modelError;
            result.ViewData.ModelState.TryGetValue("", out modelError);

            Assert.AreEqual("", result.ViewName);
            Assert.IsTrue(modelError.Errors.First().ErrorMessage == "Test");
            Assert.IsTrue(result.TempData[GlobalMessages.AddNewsSuccessKey] == null);

            mockedNewsFactory.Verify(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedNewsService.Verify(s => s.Add(It.IsAny<News>()), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);
        }

        [Test]
        public void ReturnViewWithSuccessMessage_IfAddindNewsNotFailed()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            mockedNewsFactory.Setup(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.Add(It.IsAny<News>())).Verifiable();
            mockedNewsService.Setup(s => s.Save()).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");

            var controller = new NewsController(mockedNewsFactory.Object, mockedNewsService.Object, mockedDateProvider.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddNewsViewModel();
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Index(model, mockedFile) as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsTrue(result.ViewData.ModelState.Count == 0);
            Assert.IsTrue(result.TempData[GlobalMessages.AddNewsSuccessKey] != null);

            mockedNewsFactory.Verify(f => f.CreateNews(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedNewsService.Verify(s => s.Add(It.IsAny<News>()), Times.Once);
            mockedNewsService.Verify(s => s.Save(), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);
        }
    }
}
