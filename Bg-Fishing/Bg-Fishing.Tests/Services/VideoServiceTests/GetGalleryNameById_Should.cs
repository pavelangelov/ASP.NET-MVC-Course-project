using System;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.VideoServiceTests
{
    [TestFixture]
    public class GetGalleryNameById_Should
    {
        [Test]
        public void ReturnGalleryName_IfIdMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetVideoGalleriesCollection();

            var searchedId = mockedCollection[1].Id;
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);
            
            // Act
            var galleryName = videoService.GetGalleryNameById(searchedId);

            // Assert
            Assert.AreEqual(mockedCollection[1].Name, galleryName);
        }

        [Test]
        public void ReturnNull_IfIdNotMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetVideoGalleriesCollection();

            var searchedId = Guid.NewGuid().ToString();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var galleryName = videoService.GetGalleryNameById(searchedId);

            // Assert
            Assert.IsNull(galleryName);
        }
    }
}
