using System;
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
    public class GetVideosFromGallery_Should
    {
        [Test]
        public void ReturnCorectResult_IfIdMatch()
        {
            // Arrange
            var mockedGalleriesCollection = Utils.GetVideoGalleriesCollection();

            var searchedGallery = mockedGalleriesCollection[1];
            var searchedGalleryId = searchedGallery.Id;
            searchedGallery.Videos.Add(new Video() { Title = "First video" });
            searchedGallery.Videos.Add(new Video() { Title = "Second video" });

            var mockedDbSet = MockDbSet.Mock(mockedGalleriesCollection.AsQueryable());
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            
            // Act
            var videos = videoService.GetVideosFromGallery(searchedGalleryId);

            // Assert
            Assert.IsTrue(videos.Count() == 2);
            Assert.IsTrue(videos.First().Title == "First video");
            Assert.IsTrue(videos.Last().Title == "Second video");
        }

        [Test]
        public void ReturnNull_IfIdNotMatch()
        {
            // Arrange
            var mockedGalleriesCollection = Utils.GetVideoGalleriesCollection();

            // Add videos for every gallery, so there won't be empty gallery in the context.
            foreach (var gallery in mockedGalleriesCollection)
            {
                gallery.Videos.Add(new Video() { Title = "First video" });
                gallery.Videos.Add(new Video() { Title = "Second video" });
            }
            
            var searchedGalleryId = Guid.NewGuid().ToString();

            var mockedDbSet = MockDbSet.Mock(mockedGalleriesCollection.AsQueryable());
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var videos = videoService.GetVideosFromGallery(searchedGalleryId);

            // Assert
            Assert.IsNull(videos);
        }
    }
}
