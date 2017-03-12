using Moq;
using NUnit.Framework;

using Bg_Fishing.DTOs;
using Bg_Fishing.MvcClient.Controllers.Common;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Common
{
    [TestFixture]
    public class GetVideos_Should
    {
        [Test]
        public void GetVideosFromService()
        {
            // Arrange
            var mockedVideos = new VideoDTO[]
            {
                new VideoDTO { Title = "First", Url = "FirstUrl" },
                new VideoDTO { Title = "Second", Url = "SecondUrl" }
            };
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetVideosFromGallery(It.IsAny<string>())).Returns(mockedVideos).Verifiable();

            var controller = new GalleriesController(mockedService.Object);

            // Act
            var result = controller.GetVideos(null);

            // Assert
            StringAssert.Contains(mockedVideos[0].Title, result);
            StringAssert.Contains(mockedVideos[0].Url, result);
            StringAssert.Contains(mockedVideos[1].Title, result);
            StringAssert.Contains(mockedVideos[1].Url, result);
            mockedService.Verify(s => s.GetVideosFromGallery(It.IsAny<string>()), Times.Once);
        }
    }
}
