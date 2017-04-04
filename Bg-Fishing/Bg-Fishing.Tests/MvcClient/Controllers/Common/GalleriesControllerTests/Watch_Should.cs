﻿using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers.Common;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Common.GalleriesControllerTests
{
    [TestFixture]
    public class Watch_Should
    {
        [Test]
        public void ReturnDefaultView_WithGettedVideoFromService()
        {
            // Arrange
            var mockedVideo = new VideoModel { Title = "Test video" };
            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.GetVideoById(It.IsAny<string>())).Returns(mockedVideo).Verifiable();

            var controller = new GalleriesController(mockedVideoService.Object);

            // Act
            var result = controller.Watch("some id") as ViewResult;
            var model = result.ViewData.Model as VideoModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(mockedVideo, model);
        }
    }
}
