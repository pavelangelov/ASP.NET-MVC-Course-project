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
    public class AddVideoToGallery_Should
    {
        [Test]
        public void CreateNewGallery_IfNotExist_AndAddVideoToGallery()
        {
            // Arrange
            var video = new Video() { Title = "Test video" };
            var mockedGallery = Utils.GetEmptyVideoGallery();
            var mockedDbSet = MockDbSet.Mock(mockedGallery.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<VideoGallery>())).Callback<VideoGallery>((g) => mockedGallery.Add(g));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            var newGalleryName = "Test";
            
            // Act
            videoService.AddVideoToGallery(newGalleryName, video);

            // Assert
            Assert.IsTrue(mockedDbContext.Object.VideoGalleries.Count() == 1);
            Assert.IsTrue(mockedDbContext.Object.VideoGalleries.First().Name == newGalleryName);
            Assert.IsTrue(mockedDbContext.Object.VideoGalleries.First().Videos.Count == 1);
            Assert.AreEqual(mockedDbContext.Object.VideoGalleries.First().Videos.First(), video);
        }

        [Test]
        public void AddVideoToGallery_IfGalleryExist()
        {
            // Arrange
            var video = new Video() { Title = "Test video" };
            var mockedGalleries = Utils.GetVideoGalleriesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedGalleries.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            var galleryName = mockedGalleries.First().Name;

            // Act
            videoService.AddVideoToGallery(galleryName, video);

            // Assert
            Assert.IsTrue(mockedDbContext.Object.VideoGalleries.First().Videos.Count == 1);
            Assert.AreEqual(mockedDbContext.Object.VideoGalleries.First().Videos.First(), video);
        }
    }
}
