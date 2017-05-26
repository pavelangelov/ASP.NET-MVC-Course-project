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
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfImageGalleryService_IsNull()
        {
            // Arrange
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                null,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object)).Message;

            StringAssert.Contains("imageGalleryService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfImageFactory_IsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                mockedImageGalleryService.Object,
                null,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object)).Message;

            StringAssert.Contains("imageFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDateProvider_IsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                null,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object)).Message;

            StringAssert.Contains("dateProvider", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLakeService_IsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                null,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object)).Message;

            StringAssert.Contains("lakeService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfImageGalleryFactory_IsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                null,
                mockedDirectoryHelper.Object)).Message;

            StringAssert.Contains("imageGalleryFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfDirectoryHelper_IsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                null)).Message;

            StringAssert.Contains("directoryHelper", message);
        }

        [Test]
        public void NotToThrow_IfAllDependenciesAreValid()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object));
        }
    }
}
