using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class PostAdd_Should
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

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var mockedModel = new AddVideoViewModel() { GalleryId = galleryId, VideoTitle = videoTitle };
            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var result = controller.Add(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.InvalidVideoUrlMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
            mockedVideoFactory.Verify(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            mockedDateProvider.Verify(p => p.GetDate(), Times.Never);
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

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, VideoTitle = videoTitle };
            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var result = controller.Add(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.InvalidGalleryNameMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
            mockedVideoFactory.Verify(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            mockedDateProvider.Verify(p => p.GetDate(), Times.Never);
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

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, GalleryId = galleryId };
            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);
            controller.ModelState.AddModelError("VideoTitle", "Error");

            // Act
            var result = controller.Add(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.InvalidVideoTitleMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Never);
            mockedVideoFactory.Verify(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
            mockedDateProvider.Verify(p => p.GetDate(), Times.Never);
        }

        [Test]
        public void CallAddVideoToGalleryFromVideoService_AndReturnSuccessMessage_IfVideoIsAdded()
        {
            // Arrange
            var galleryName = "Valid name";
            var videoUrl = "url";
            var galleryId = "id";
            var videoTitle = "Title";
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetGalleryNameById(It.IsAny<string>())).Returns(galleryName).Verifiable();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();
            mockedService.Setup(s => s.Save()).Verifiable();

            var mockedVideoFactory = new Mock<IVideoFactory>();
            mockedVideoFactory.Setup(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(p => p.GetDate()).Verifiable();

            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);
            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, GalleryId = galleryId, VideoTitle = videoTitle };

            // Act
            var result = controller.Add(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.AddVideoSuccessMessage, dResult.message);
            Assert.AreEqual("success", dResult.status);

            mockedService.Verify(s => s.GetGalleryNameById(It.IsAny<string>()), Times.Once);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Once);
            mockedService.Verify(s => s.Save(), Times.Once);

            mockedDateProvider.Verify(p => p.GetDate(), Times.Once);

            mockedVideoFactory.Verify(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void ReturnErrorMessage_IfVideoIsNotAdded()
        {
            // Arrange
            var galleryName = "Valid name";
            var videoUrl = "url";
            var galleryId = "id";
            var videoTitle = "Title";
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetGalleryNameById(It.IsAny<string>())).Returns(galleryName).Verifiable();
            mockedService.Setup(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>())).Verifiable();
            mockedService.Setup(s => s.Save()).Throws<Exception>();

            var mockedVideoFactory = new Mock<IVideoFactory>();
            mockedVideoFactory.Setup(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(p => p.GetDate()).Verifiable();

            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);
            var mockedModel = new AddVideoViewModel() { VideoUrl = videoUrl, GalleryId = galleryId, VideoTitle = videoTitle };

            // Act
            var result = controller.Add(mockedModel) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.AddVideoErrorMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);

            mockedService.Verify(s => s.GetGalleryNameById(It.IsAny<string>()), Times.Once);
            mockedService.Verify(s => s.AddVideoToGallery(It.IsAny<string>(), It.IsAny<Video>()), Times.Once);

            mockedDateProvider.Verify(p => p.GetDate(), Times.Once);

            mockedVideoFactory.Verify(f => f.CreateVideo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
