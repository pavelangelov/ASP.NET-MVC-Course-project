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
    public class FindById_Should
    {
        [Test]
        public void ReturnCorrectResult_IfIdMatch()
        {
            // Arrange
            var gallery = new ImageGallery() { Name = "Test gallery" };

            var mockedCollection = new List<ImageGallery>()
            {
                new ImageGallery(),
                gallery,
                new ImageGallery()
            };

            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            var result = imageGalleryService.FindById(gallery.Id);

            // Assert
            Assert.AreEqual(gallery, result);
        }
    }
}
