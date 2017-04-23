using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.NewsServiceTests
{
    [TestFixture]
    public class GetNewsById_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var mockedCollection = Utils.GetNewsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.News).Returns(mockedDbSet.Object);

            var newsService = new NewsService(mockedDbContext.Object);
            var searchedNews = mockedCollection[1];

            // Act
            var result = newsService.GetNewsById(searchedNews.Id);

            // Assert
            Assert.AreEqual(searchedNews.Title, result.Title);
            Assert.AreEqual(searchedNews.Id, result.Id);
        }
    }
}
