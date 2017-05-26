using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.PicturesControllerTests
{
    [TestFixture]
    public class GetConfirm_Should
    {
        [Test]
        public void GetGalleriesWithunconfirmedImages_FromDatabase_AndRenderDefaultView()
        {
            // Arrange
            var mockedCollection = new List<ImageGalleryModel>();
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(s => s.GetGalleriesWithUnconfirmedImages())
                                        .Returns(mockedCollection)
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

            // Act
            var result = controller.Confirm() as ViewResult;
            var model = result.Model as IEnumerable<ImageGalleryModel>;

            // Assert
            Assert.AreEqual(mockedCollection, model);
            Assert.IsTrue(result.ViewName == "");

            mockedImageGalleryService.Verify(s => s.GetGalleriesWithUnconfirmedImages(), Times.Once);
        }
    }
}
