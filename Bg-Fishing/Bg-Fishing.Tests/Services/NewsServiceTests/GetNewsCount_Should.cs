using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.NewsServiceTests
{
    [TestFixture]
    public class GetNewsCount_Should
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

            // Act
            var result = newsService.GetNewsCount();

            // Assert
            Assert.AreEqual(mockedCollection.Count, result);
        }
    }
}
