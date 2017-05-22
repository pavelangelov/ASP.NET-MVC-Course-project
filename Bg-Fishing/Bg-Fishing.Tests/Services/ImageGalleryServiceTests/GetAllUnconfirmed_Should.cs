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
    public class GetAllUnconfirmed_Should
    {
        [Test]
        public void ReturnAllUnconfirmedImages()
        {
            // Arrange
            var mockedImageCollection = new List<Image>()
            {
                new Image() { IsConfirmed = false },
                new Image() { IsConfirmed = true },
                new Image() { IsConfirmed = false }
            };

            var mockedGalleryCollection = new List<ImageGallery>()
            {
                new ImageGallery(),
                new ImageGallery(){ Images = mockedImageCollection },
                new ImageGallery()
            };
            var mockedDbSet = MockDbSet.Mock(mockedGalleryCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedGalleryCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);
            var searchedGalleryId = mockedGalleryCollection[1].Id;

            // Act
            var result = imageGalleryService.GetAllUnconfirmed(searchedGalleryId);

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }
    }
}
