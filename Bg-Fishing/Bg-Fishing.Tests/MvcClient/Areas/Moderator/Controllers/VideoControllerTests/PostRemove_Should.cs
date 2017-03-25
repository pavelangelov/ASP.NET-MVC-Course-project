using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class PostRemove_Should
    {
        [Test]
        public void ReturnJasonWithCorrectErrorMessage_IfRemovingVideoFailed()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.RemoveVideoFromGallery(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            mockedVideoService.Setup(s => s.Save()).Throws<Exception>();

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var view = controller.Remove(It.IsAny<string>(), It.IsAny<string>()) as JsonResult;
            dynamic dResult = view.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual(GlobalMessages.RemoveVideoErroMessage, dResult.message);
            mockedVideoService.Verify(s => s.RemoveVideoFromGallery(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnJasonWithCorrectSuccessMessage_IfRemovingVideoNotFailed()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.RemoveVideoFromGallery(It.IsAny<string>(), It.IsAny<string>())).Returns(true).Verifiable();
            mockedVideoService.Setup(s => s.Save()).Verifiable();

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var view = controller.Remove(It.IsAny<string>(), It.IsAny<string>()) as JsonResult;
            dynamic dResult = view.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(GlobalMessages.RemoveVideoSuccessMessage, dResult.message);
            mockedVideoService.Verify(s => s.RemoveVideoFromGallery(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockedVideoService.Verify(s => s.Save(), Times.Once);
        }
    }
}
