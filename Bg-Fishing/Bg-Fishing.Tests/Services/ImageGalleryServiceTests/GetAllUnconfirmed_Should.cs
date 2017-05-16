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
            var mockedCollection = new List<Image>()
            {
                new Image() { IsConfirmed = false },
                new Image() { IsConfirmed = true },
                new Image() { IsConfirmed = false }
            };
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Images).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            var result = imageGalleryService.GetAllUnconfirmed();

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }
    }
}
