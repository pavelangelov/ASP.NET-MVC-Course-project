using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.NewsServiceTests
{
    [TestFixture]
    public class GetNews_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var mockedCollection = Utils.GetNewsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.News).Returns(mockedDbSet.Object);

            var newsService = new NewsService(mockedDbContext.Object);
            var expectedNews = mockedCollection[1];

            // Act
            var result = newsService.GetNews(1, 1);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(expectedNews.Title, result.First().Title);
            Assert.AreEqual(expectedNews.Id, result.First().Id);
        }

        [Test]
        public void ReturnCorrectResult_IfParameterIsOutOfRabge()
        {
            // Arrange
            var mockedCollection = Utils.GetNewsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.News).Returns(mockedDbSet.Object);

            var newsService = new NewsService(mockedDbContext.Object);

            // Act
            var result = newsService.GetNews(3, 1);

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
