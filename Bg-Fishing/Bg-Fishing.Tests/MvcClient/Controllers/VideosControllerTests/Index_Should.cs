using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models.ViewModels.Common;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.Controllers.VideosControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void GetVideoGalleriesFromService_AndSetToViewModel()
        {
            // Arrange
            var mockedDTO = new VideoGalleryModel { Id = "1", Name = "First" };
            var galleries = new VideoGalleryModel[] { mockedDTO };
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetAll()).Returns(galleries).Verifiable();

            var controller = new VideosController(mockedService.Object);

            // Act
            var view = controller.Index() as ViewResult;
            var model = view.ViewData.Model as VideoGalleriesViewModel;

            // Assert
            Assert.IsTrue(model.Galleries.Count() == 2);
            Assert.IsTrue(model.Galleries.First().Value == "");
            Assert.IsTrue(model.Galleries.Last().Value == mockedDTO.Id);
            Assert.IsTrue(model.Galleries.Last().Text == mockedDTO.Name);
            mockedService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
