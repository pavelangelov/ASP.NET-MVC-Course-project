using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.VideoServiceTests
{
    [TestFixture]
    public class GetVideoById_Should
    {
        [Test]
        public void ReturnCorrectResult_IfIdMatch()
        {
            // Arrange
            var mockedColletion = Utils.GetVideoCollection();
            var mockedDbSet = MockDbSet.Mock(mockedColletion.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Videos).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            var searchedVideo = mockedColletion[1];

            // Act
            var result = videoService.GetVideoById(searchedVideo.Id);

            // Assert
            Assert.AreEqual(searchedVideo.Id, result.Id);
            Assert.AreEqual(searchedVideo.Title, result.Title);
            Assert.AreEqual(searchedVideo.Url, result.Url);
        }

        [Test]
        public void ReturnCorrectResult_IfIdNotMatch()
        {
            // Arrange
            var mockedColletion = Utils.GetVideoCollection();
            var mockedDbSet = MockDbSet.Mock(mockedColletion.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Videos).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var result = videoService.GetVideoById("some id");

            // Assert
            Assert.IsNull(result);
        }
    }
}
