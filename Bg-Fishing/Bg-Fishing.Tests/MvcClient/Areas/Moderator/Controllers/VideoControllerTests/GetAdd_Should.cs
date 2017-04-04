using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class GetAdd_Should
    {
        [Test]
        public void GetAllGalleryNames_AndStoreItToViewModel()
        {
            // Arrange
            var galleryNames = new VideoGalleryModel[] { new VideoGalleryModel { Name = "Test" }, new VideoGalleryModel { Name = "Test2" } };
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetAll()).Returns(galleryNames).Verifiable();
            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var controller = new VideoController(mockedService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var view = controller.Add() as ViewResult;
            var model = view.ViewData.Model as AddVideoViewModel;

            // Assert
            Assert.IsAssignableFrom<AddVideoViewModel>(model);
            Assert.IsTrue(model.GalleryNames.Count() == 3);
            Assert.IsTrue(model.GalleryNames.Last().Text == "Test2");
        }
    }
}
