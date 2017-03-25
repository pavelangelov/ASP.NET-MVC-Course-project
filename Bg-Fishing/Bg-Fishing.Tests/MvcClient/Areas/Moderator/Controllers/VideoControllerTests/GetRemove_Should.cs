using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.DTOs;
using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class GetRemove_Should
    {
        [Test]
        public void GetAllVideoGalleriesFromService_AndRenderDefaultView()
        {
            // Arrange
            var mockedCollection = new List<GalleryDTO>
            {
                new GalleryDTO { Name = "Test" }
            };

            var mockedVideoService = new Mock<IVideoService>();
            mockedVideoService.Setup(s => s.GetAll()).Returns(mockedCollection).Verifiable();

            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            // Act
            var view = controller.Remove() as ViewResult;
            var model = view.ViewData.Model as RemoveVideoViewModel;

            // Assert
            Assert.IsTrue(view.ViewName == "");
            CollectionAssert.AreEqual(mockedCollection, model.Galleries);
        }
    }
}
