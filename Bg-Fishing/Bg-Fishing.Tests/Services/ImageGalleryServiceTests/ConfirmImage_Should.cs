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
    public class ConfirmImage_Should
    {
        [Test]
        public void SetImageIsConfirmed_ToTrue_IfIdMatch()
        {
            // Arrange
            var mockedCollection = new List<Image>()
            {
                new Image(){ IsConfirmed = true },
                new Image(){ IsConfirmed = false },
                new Image(){ IsConfirmed = true },
            };
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Images).Returns(mockedDbSet.Object);
            var service = new ImageGalleryService(mockedDbContext.Object);
            var searchedImageId = mockedCollection[1].Id;

            // Act
            service.ConfirmImage(searchedImageId);

            // Assert
            Assert.IsTrue(mockedCollection[1].IsConfirmed);
        }
    }
}
