using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.PicturesControllerTests
{
    [TestFixture]
    public class PutConfirm_Should
    {
        [Test]
        public void ReturnError_IfConfirmingImageFailed()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(s => s.ConfirmImage(It.IsAny<string>()))
                                        .Throws<NullReferenceException>();

            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            var controller = new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);

            // Act
            var result = controller.Confirm(It.IsAny<string>());

            // Assert
            StringAssert.Contains("error", result);
            StringAssert.Contains("Опитвате се да потвърдите невалидно изображение!", result);
        }

        [Test]
        public void ReturnSuccess_IfConfirmingImageNotFailed()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(s => s.ConfirmImage(It.IsAny<string>()))
                                        .Verifiable();

            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();
            
            var controller = new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);

            var mockedImageId = "Mocked Image Id";

            // Act
            var result = controller.Confirm(mockedImageId);

            // Assert
            StringAssert.Contains("success", result);
            StringAssert.Contains("Изображението е потвърдено", result);

            mockedImageGalleryService.Verify(s => s.ConfirmImage(mockedImageId), Times.Once);
        }
    }
}
