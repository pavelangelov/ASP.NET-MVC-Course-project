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
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel();
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
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel() { VideoUrl = "valid" };
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
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();

            var mockedModel = new AddVideoViewModel() { VideoUrl = "valid", GalleryName = "valid" };
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
