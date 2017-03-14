using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.VideoServiceTests
{
    [TestFixture]
    public class RemoveVideoFromGallery_Should
    {
        [Test]
        public void ReturnTrue_IfGalleryExist_AndVideoIsRemoved()
        {
            // Arrange
            var mockedGalleries = Utils.GetVideoGalleriesCollection();
            var video = new Video();
            var gallery = mockedGalleries.First();
            gallery.Videos.Add(video);

            var mockedDbSet = MockDbSet.Mock(mockedGalleries.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Confirm that the gallery contains the video before remove it
            Assert.IsTrue(gallery.Videos.Count == 1);
            Assert.AreEqual(gallery.Videos.First(), video);

            // Act
            var isRemoved = videoService.RemoveVideoFromGallery(gallery.Name, video.Id);

            Assert.IsTrue(gallery.Videos.Count == 0);
            Assert.IsTrue(isRemoved);
        }

        [Test]
        public void ReturnFalse_IfGalleryNotExist()
        {
            // Arrange
            var mockedGalleries = Utils.GetVideoGalleriesCollection();
            var video = new Video();

            var mockedDbSet = MockDbSet.Mock(mockedGalleries.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            
            // Act
            var isRemoved = videoService.RemoveVideoFromGallery("Some name", video.Id);

            Assert.IsFalse(isRemoved);
        }

        [Test]
        public void ReturnFalse_IfGalleryNotContainsTheVideo()
        {
            // Arrange
            var mockedGalleries = Utils.GetVideoGalleriesCollection();
            var video = new Video();
            var gallery = mockedGalleries.First();

            var mockedDbSet = MockDbSet.Mock(mockedGalleries.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var isRemoved = videoService.RemoveVideoFromGallery(gallery.Name, video.Id);

            Assert.IsFalse(isRemoved);
        }
    }
}
