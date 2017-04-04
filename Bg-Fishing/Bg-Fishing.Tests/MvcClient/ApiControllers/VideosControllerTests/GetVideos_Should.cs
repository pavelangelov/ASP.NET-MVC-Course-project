using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.VideosControllerTests
{
    [TestFixture]
    public class GetVideos_Should
    {
        [Test]
        public void ReturnEmptyCollection_IfGalleryIdNotMatch()
        {
            // Arrange
            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.GetVideosFromGallery(It.IsAny<string>())).Verifiable();

            var controller = new VideosController(mockedVideoService.Object);

            // Act
            var result = controller.GetVideos(It.IsAny<string>());

            // Assert
            Assert.IsTrue(result.Count() == 0);
            mockedVideoService.Verify(s => s.GetVideosFromGallery(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_IfGalleryIdMatch()
        {
            // Arrange
            var mockedCollection = new List<VideoModel> { new VideoModel { Title = "Test", Url = "Test" } };
            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.GetVideosFromGallery(It.IsAny<string>())).Returns(mockedCollection).Verifiable();

            var controller = new VideosController(mockedVideoService.Object);

            // Act
            var result = controller.GetVideos(It.IsAny<string>());

            // Assert
            CollectionAssert.AreEqual(mockedCollection, result);
            mockedVideoService.Verify(s => s.GetVideosFromGallery(It.IsAny<string>()), Times.Once);
        }
    }
}
