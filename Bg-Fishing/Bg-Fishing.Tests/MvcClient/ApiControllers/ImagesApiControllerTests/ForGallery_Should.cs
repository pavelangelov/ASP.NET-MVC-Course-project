using System.Collections.Generic;

using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.ImagesApiControllerTests
{
    [TestFixture]
    public class ForGallery_Should
    {
        [Test]
        public void GetUnconfirmedImages_ForThisGalleryId_AndReturnItAsJsonString()
        {
            // Arrange
            var mockedCollection = new List<ImageModel>() { new ImageModel() };
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(s => s.GetAllUnconfirmed(It.IsAny<string>()))
                                        .Returns(mockedCollection)
                                        .Verifiable();

            var controller = new ImagesApiController(mockedImageGalleryService.Object);
            var searchedGalleryId = "Mocked Gallery Id";
            var expectedResult = JsonConvert.SerializeObject(mockedCollection);

            // Act
            var result = controller.GetUnconfirmedFromGallery(searchedGalleryId);

            // Assert
            StringAssert.Contains(expectedResult, result);

            mockedImageGalleryService.Verify(s => s.GetAllUnconfirmed(searchedGalleryId), Times.Once);
        }
    }
}
