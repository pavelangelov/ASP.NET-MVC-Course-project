using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

using Moq;
using Microsoft.AspNet.SignalR.Hubs;
using NUnit.Framework;

using Bg_Fishing.DTOs;
using Bg_Fishing.MvcClient.Hubs;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.MvcClient.Hubs.NewsHubTests
{
    [TestFixture]
    public class GetNews_Should
    {
        [Test]
        public void ReturnCorrectResult_IfHasMoreNewsToShow()
        {
            // Arrange
            var mockedCollection = new List<NewsDTO> { new NewsDTO { Title = "Test" } };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.GetNews(It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();
            mockedNewsService.Setup(s => s.GetNewsCount()).Returns(mockedCollection.Count + Constants.ShowedNewsCount);

            var hub = new NewsHub(mockedNewsService.Object);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;

            var loadNewsCalled = false;
            IEnumerable<NewsDTO> sendedNews = Enumerable.Empty<NewsDTO>();
            bool hasMoreSended = false;
            int nextPageSended = 0;

            dynamic caller = new ExpandoObject();
            caller.loadNews = new Action<IEnumerable<NewsDTO>, bool, int>((news, hasMore, nextPage) =>
            {
                loadNewsCalled = true;
                sendedNews = news;
                hasMoreSended = hasMore;
                nextPageSended = nextPage;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            // Act
            hub.GetNews(It.IsAny<int>());

            // Assert
            Assert.IsTrue(loadNewsCalled);
            Assert.IsTrue(hasMoreSended);
            Assert.IsTrue(nextPageSended  == 1);
            Assert.AreEqual(sendedNews, mockedCollection);
        }

        [Test]
        public void ReturnCorrectResult_IsHasNowMoreNewsToShow()
        {
            // Arrange
            var mockedCollection = new List<NewsDTO> { new NewsDTO { Title = "Test" } };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(s => s.GetNews(It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();
            mockedNewsService.Setup(s => s.GetNewsCount()).Returns(mockedCollection.Count);

            var hub = new NewsHub(mockedNewsService.Object);
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;

            var loadNewsCalled = false;
            IEnumerable<NewsDTO> sendedNews = Enumerable.Empty<NewsDTO>();
            bool hasMoreSended = false;
            int nextPageSended = 0;

            dynamic caller = new ExpandoObject();
            caller.loadNews = new Action<IEnumerable<NewsDTO>, bool, int>((news, hasMore, nextPage) =>
            {
                loadNewsCalled = true;
                sendedNews = news;
                hasMoreSended = hasMore;
                nextPageSended = nextPage;
            });

            mockClients.Setup(m => m.Caller).Returns((ExpandoObject)caller);

            // Act
            hub.GetNews(It.IsAny<int>());

            // Assert
            Assert.IsTrue(loadNewsCalled);
            Assert.IsFalse(hasMoreSended);
            Assert.IsTrue(nextPageSended == 0);
            Assert.AreEqual(sendedNews, mockedCollection);
        }
    }
}
