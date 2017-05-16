using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.ImageGalleryServiceTests
{
    [TestFixture]
    public class AddImageToGallery_Should
    {
        [Test]
        public void AddImageToGallery_IfIdMatch()
        {
            // Arrange
            var image = new Image();
            var gallery = new ImageGallery() { Name = "Test gallery" };

            var mockedCollection = new List<ImageGallery>() { gallery };
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            imageGalleryService.AddImageToGallery(image, gallery.Id);

            // Assert
            Assert.IsTrue(gallery.Images.Count == 1);
            Assert.IsTrue(gallery.Images.Contains(image));
        }

        [Test]
        public void AddImageToGallery_IfNameMatch()
        {
            // Arrange
            var image = new Image();
            var gallery = new ImageGallery() { Name = "Test gallery" };

            var mockedCollection = new List<ImageGallery>() { gallery };
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            imageGalleryService.AddImageToGallery(gallery.Name, image);

            // Assert
            Assert.IsTrue(gallery.Images.Count == 1);
            Assert.IsTrue(gallery.Images.Contains(image));
        }
    }
}
