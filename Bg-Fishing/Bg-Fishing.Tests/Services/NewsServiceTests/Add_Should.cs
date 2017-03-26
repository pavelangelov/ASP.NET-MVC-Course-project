using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.NewsServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        public void AddNewsToDbContext()
        {
            // Arrange
            var mockedCollection = new List<News>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<News>())).Callback<News>(n => mockedCollection.Add(n));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.News).Returns(mockedDbSet.Object);

            var newsService = new NewsService(mockedDbContext.Object);
            var news = new News();

            // Act
            newsService.Add(news);

            // Assert
            Assert.IsTrue(mockedCollection.Count == 1);
            Assert.IsTrue(mockedCollection.Contains(news));
        }
    }
}
