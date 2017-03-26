using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.DTOs;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void GetLatestNewsFromService_AndRenderDefaultView()
        {
            // Arrange
            var mockedCollection = new List<NewsDTO>
            {
                new NewsDTO { Title = "First" },
                new NewsDTO { Title = "Second" },
                new NewsDTO { Title = "Third" }
            };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.GetNews(It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();
            mockedNewsService.Setup(s => s.GetNewsCount()).Returns(0);

            var controller = new HomeController(mockedNewsService.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as HomeViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(mockedCollection, model.News);
            Assert.AreEqual(0, model.NextPage);
            Assert.IsFalse(model.HasMoreNews);

            mockedNewsService.Verify(s => s.GetNews(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockedNewsService.Verify(s => s.GetNewsCount(), Times.Once);
        }

        [Test]
        public void SetModel_HasMoreNewsToTrue_IfHasOtherNewsToShow()
        {
            // Arrange
            var mockedCollection = new List<NewsDTO>
            {
                new NewsDTO { Title = "First" },
                new NewsDTO { Title = "Second" },
                new NewsDTO { Title = "Third" }
            };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.GetNews(It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();
            mockedNewsService.Setup(s => s.GetNewsCount()).Returns(mockedCollection.Count * 2);

            var controller = new HomeController(mockedNewsService.Object);

            // Act
            var result = controller.Index(0) as ViewResult;
            var model = result.ViewData.Model as HomeViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(1, model.NextPage);
            Assert.IsTrue(model.HasMoreNews);
        }
    }
}
