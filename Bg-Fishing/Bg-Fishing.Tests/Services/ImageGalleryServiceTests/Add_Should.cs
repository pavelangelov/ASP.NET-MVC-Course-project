using Bg_Fishing.Data;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.ImageGalleryServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void AddImageGalleryToDbContext()
        {
            // Arrange
            var gallery = new ImageGallery() { Name = "Test gallery" };
            var mockedCollection = new List<ImageGallery>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<ImageGallery>())).Callback<ImageGallery>((g) => mockedCollection.Add(g));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGallertService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            imageGallertService.Add(gallery);

            // Assert
            Assert.IsTrue(mockedCollection.Count == 1);
            Assert.AreEqual(gallery, mockedCollection[0]);
        }
    }
}
