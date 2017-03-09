using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers.Moderator;
using Bg_Fishing.MvcClient.Models.ViewModels.Moderator;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Moderator.AddVideoControllerTests
{
    [TestFixture]
    public class GetIndex_Should
    {
        [Test]
        public void GetAllGalleryNames_AndStoreItToViewModel()
        {
            // Arrange
            var galleryNames = new string[] { "Test", "Test2" };
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetAll()).Returns(galleryNames).Verifiable();
            var controller = new AddVideoController(mockedService.Object);

            // Act
            var view = controller.Index() as ViewResult;
            var model = view.ViewData.Model as AddVideoViewModel;

            // Assert
            Assert.IsAssignableFrom<AddVideoViewModel>(model);
            Assert.IsTrue(model.GalleryNames.Count() == 3);
            Assert.IsTrue(model.GalleryNames.Last().Value == "Test2");
        }
    }
}
