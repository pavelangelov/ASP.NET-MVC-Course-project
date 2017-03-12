using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Models.Galleries;
using Bg_Fishing.MvcClient.Controllers.Moderator;
using Bg_Fishing.MvcClient.Models.ViewModels.Moderator;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Moderator.AddVideoControllerTests
{
    [TestFixture]
    public class PostIndex_Should
    {
        [Test]
        public void ReturnJsonWithErrorMessage_IfVideoLinkIsNotValid()
        {
            // Arrange
            var galleryId = "id";
            var videoTitle = "Title";
            var galleryName = "Valid name";
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetGalleryNameById(It.IsAny<string>())).Returns(galleryName);
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel() { GalleryId = galleryId, VideoTitle = videoTitle };
            var controller = new AddVideoController(mockedService.Object);

            // Act
            var result = controller.Index(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("Линкът към видеото е невалиден.", dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithErrorMessage_IfGalleryNameIsNull()
        {
            // Arrange
            var videoTitle = "Title";
            var videoUrl = "url";
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetGalleryNameById(It.IsAny<string>())).Verifiable();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, VideoTitle = videoTitle };
            var controller = new AddVideoController(mockedService.Object);

            // Act
            var result = controller.Index(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("Не е избрана категория.", dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
        }


        [Test]
        public void ReturnJsonWithErrorMessage_IfVideoTitleIsNotValid()
        {
            // Arrange
            var galleryName = "Valid name";
            var videoUrl = "url";
            var galleryId = "id";
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetGalleryNameById(It.IsAny<string>())).Returns(galleryName);
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, GalleryId = galleryId };
            var controller = new AddVideoController(mockedService.Object);
            controller.ModelState.AddModelError("VideoTitle", "Error");

            // Act
            var result = controller.Index(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("Невалидно загалвие на видеото.", dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
        }

        // TODO: Add tests for adding video to DB when Factories are implemented
    }
}
