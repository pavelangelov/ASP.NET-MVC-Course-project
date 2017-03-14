using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.VideoServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var mockedGalleriesCollection = Utils.GetVideoGalleriesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedGalleriesCollection.AsQueryable());
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.VideoGalleries).Returns(mockedDbSet.Object);

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var galleries = videoService.GetAll();

            // Assert
            Assert.IsNotNull(galleries);
            Assert.AreEqual(mockedGalleriesCollection.Count, galleries.Count());
            int index = 0;
            foreach (var gallery in galleries)
            {
                Assert.AreEqual(mockedGalleriesCollection[index].Name, gallery.Name);
                Assert.AreEqual(mockedGalleriesCollection[index].Id, gallery.GalleryId);
                index++;
            }
        }

        [Test]
        public void ReturnNull_IfCollectionIsEmpty()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();

            var videoService = new VideoService(mockedDbContext.Object);

            // Act
            var galleries = videoService.GetAll();

            // Assert
            Assert.IsNull(galleries);
        }
    }
}
