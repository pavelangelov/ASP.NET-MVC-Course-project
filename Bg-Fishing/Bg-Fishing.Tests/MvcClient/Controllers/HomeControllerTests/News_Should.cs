using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.HomeControllerTests
{
    [TestFixture]
    public class News_Should
    {
        [Test]
        public void GetNewsFromService_AndRenderDefaultView()
        {
            // Arrange
            var mockedNewsModel = new NewsModel();
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.GetNewsById(It.IsAny<string>())).Returns(mockedNewsModel).Verifiable();

            var mockedNewsCommentFactory = new Mock<INewsCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new HomeController(mockedNewsService.Object, mockedNewsCommentFactory.Object, mockedDateProvider.Object);
            var newsId = "test id";

            // Act
            var result = controller.News(newsId) as ViewResult;
            var model = result.ViewData.Model as NewsDetailsViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(mockedNewsModel, model.News);
            mockedNewsService.Verify(s => s.GetNewsById(newsId), Times.Once);
        }
    }
}
