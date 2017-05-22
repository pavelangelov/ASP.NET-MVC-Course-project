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
    public class GetGalleriesWithUnconfirmedImages_Should
    {
        [Test]
        public void ReturnOnlyGalleries_ThatHaveUnconfirmedImages()
        {
            // Arrange
            var mockedCollection = this.GetGalleries();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var service = new ImageGalleryService(mockedDbContext.Object);

            // Act
            var result = service.GetGalleriesWithUnconfirmedImages();

            // Assert
            Assert.IsTrue(result.Count() == 1);
        }

        private IList<ImageGallery> GetGalleries()
        {
            var collectionWithAllConfirmedImages = new List<Image>()
            {
                new Image(){ IsConfirmed = true},
                new Image(){ IsConfirmed = true},
                new Image(){ IsConfirmed = true}
            };
            var collectionWithUnconfirmedImages = new List<Image>()
            {
                new Image(){ IsConfirmed = true},
                new Image(){ IsConfirmed = false},
                new Image(){ IsConfirmed = true},
            };

            return new List<ImageGallery>()
            {
                new ImageGallery() { Images = collectionWithAllConfirmedImages },
                new ImageGallery() { Images = collectionWithUnconfirmedImages },
                new ImageGallery(){ Images = collectionWithAllConfirmedImages }
            };
        }
    }
}
