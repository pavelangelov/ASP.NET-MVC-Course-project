﻿using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.DTOs;
using Bg_Fishing.MvcClient.Controllers.Common;
using Bg_Fishing.MvcClient.Models.ViewModels.Common;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.Common.GalleriesControllerTests
{
    [TestFixture]
    public class Videos_Should
    {
        [Test]
        public void GetVideoGalleriesFromService_AndSetToViewModel()
        {
            // Arrange
            var mockedDTO = new GalleryDTO { GalleryId = "1", Name = "First" };
            var galleries = new GalleryDTO[] { mockedDTO };
            var mockedService = new Mock<IVideoService>();
            mockedService.Setup(s => s.GetAll()).Returns(galleries).Verifiable();

            var controller = new GalleriesController(mockedService.Object);

            // Act
            var view = controller.Videos() as ViewResult;
            var model = view.ViewData.Model as VideoGalleriesViewModel;

            // Assert
            Assert.IsTrue(model.Galleries.Count() == 2);
            Assert.IsTrue(model.Galleries.First().Value == "");
            Assert.IsTrue(model.Galleries.Last().Value == mockedDTO.GalleryId);
            Assert.IsTrue(model.Galleries.Last().Text == mockedDTO.Name);
            mockedService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}