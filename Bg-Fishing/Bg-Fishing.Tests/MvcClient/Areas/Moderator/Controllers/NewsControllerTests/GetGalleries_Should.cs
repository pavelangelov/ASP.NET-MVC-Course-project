using System.Collections.Generic;

using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.NewsControllerTests
{
    [TestFixture]
    public class GetGalleries_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var mockedGalleryCollection = new List<ImageGalleryModel>() { new ImageGalleryModel() };

            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(g => g.GetByLake(It.IsAny<string>()))
                                        .Returns(mockedGalleryCollection)
                                        .Verifiable();

            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);

            var result = controller.GetGalleries("test") as string;
            var expectedResult = JsonConvert.SerializeObject(mockedGalleryCollection);
            
            // Assert
            Assert.AreEqual(expectedResult, result);
            mockedImageGalleryService.Verify(s => s.GetByLake("test"), Times.Once);
        }
    }
}
