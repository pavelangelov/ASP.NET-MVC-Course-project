using System;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Tests.MvcClient.Mocks;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.ImageControllerTests
{
    public class PostAdd_Should
    {
        [Test]
        public void AddErrorToModelState_IfFileIsNull()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);

            var model = new AddImageViewModel();

            // Act
            var result = controller.Add(null, model) as ViewResult;

            // Assert
            Assert.AreEqual(
                GlobalMessages.NoFileErrorMessage, 
                result.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Test]
        public void AddErrorMessageToModel_IfAddingImageFail()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Throws(new ArgumentException(message: "Test"));

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");
            mockedHttpContext.Setup(c => c.HttpContext.User.IsInRole(It.IsAny<string>())).Returns(true);

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddImageViewModel();
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Add(mockedFile, model) as ViewResult;

            // Assert
            Assert.AreEqual(
                "Test",
                result.ViewData.ModelState[""].Errors.First().ErrorMessage);
        }

        [Test]
        public void GetGalleryFromService_AndAddImageToGallery()
        {
            // Arrange
            var mockedGallery = new ImageGallery();
            var mockedImage = new Image();

            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(g => g.FindById(It.IsAny<string>()))
                                        .Returns(mockedGallery)
                                        .Verifiable();
            mockedImageGalleryService.Setup(s => s.Save()).Verifiable();

            var mockedImageFactory = new Mock<IImageFactory>();
            mockedImageFactory.Setup(f => f.CreateImage(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                                .Returns(mockedImage)
                                .Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            mockedImageGalleryFactory.Setup(f => f.CreateImageGallery(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();
            mockedDirectoryHelper.Setup(d => d.CreateIfNotExist(It.IsAny<string>())).Verifiable();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");
            mockedHttpContext.Setup(c => c.HttpContext.User.IsInRole(It.IsAny<string>())).Returns(true);

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddImageViewModel();
            model.SelectedImageGalleryId = "testId";
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Add(mockedFile, model) as ViewResult;

            // Assert
            Assert.IsTrue(mockedGallery.Images.Contains(mockedImage));
            Assert.IsTrue(mockedFile.IsSaveAsCalled);

            mockedImageGalleryService.Verify(s => s.Save(), Times.Once);
            mockedImageGalleryService.Verify(s => s.FindById("testId"), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);

            mockedLakeService.Verify(l => l.GetAll(), Times.Once);

            mockedImageGalleryFactory.Verify(f => f.CreateImageGallery(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            mockedDirectoryHelper.Verify(d => d.CreateIfNotExist("Test"), Times.Once);
        }

        [Test]
        public void CreateGallery_IfNotExist_AndAddImageToGallery()
        {
            // Arrange
            var mockedGallery = new ImageGallery();
            var mockedImage = new Image();

            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(g => g.FindById(It.IsAny<string>()))
                                        .Verifiable();
            mockedImageGalleryService.Setup(s => s.Save()).Verifiable();

            var mockedImageFactory = new Mock<IImageFactory>();
            mockedImageFactory.Setup(f => f.CreateImage(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                                .Returns(mockedImage)
                                .Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            mockedImageGalleryFactory.Setup(f => f.CreateImageGallery(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockedGallery);

            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();
            mockedDirectoryHelper.Setup(d => d.CreateIfNotExist(It.IsAny<string>())).Verifiable();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");
            mockedHttpContext.Setup(c => c.HttpContext.User.IsInRole(It.IsAny<string>())).Returns(true);

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddImageViewModel();
            model.SelectedImageGalleryId = "testId";
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Add(mockedFile, model) as ViewResult;

            // Assert
            Assert.IsTrue(mockedGallery.Images.Contains(mockedImage));
            Assert.IsTrue(mockedFile.IsSaveAsCalled);

            mockedImageGalleryService.Verify(s => s.Save(), Times.Once);
            mockedImageGalleryService.Verify(s => s.FindById("testId"), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);

            mockedLakeService.Verify(l => l.GetAll(), Times.Once);

            mockedImageGalleryFactory.Verify(f => f.CreateImageGallery(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockedDirectoryHelper.Verify(d => d.CreateIfNotExist("Test"), Times.Once);
        }


        [Test]
        public void LeaveImageAsUnconfirmed_IfUserIsNotInRoleAsModerator()
        {
            // Arrange
            var mockedGallery = new ImageGallery();
            var mockedImage = new Image();

            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            mockedImageGalleryService.Setup(g => g.FindById(It.IsAny<string>()))
                                        .Returns(mockedGallery)
                                        .Verifiable();
            mockedImageGalleryService.Setup(s => s.Save()).Verifiable();

            var mockedImageFactory = new Mock<IImageFactory>();
            mockedImageFactory.Setup(f => f.CreateImage(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                                .Returns(mockedImage)
                                .Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(d => d.GetDate()).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();
            mockedDirectoryHelper.Setup(d => d.CreateIfNotExist(It.IsAny<string>())).Verifiable();

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");
            mockedHttpContext.Setup(c => c.HttpContext.User.IsInRole(It.IsAny<string>())).Returns(false);

            var controller = new ImageController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);
            controller.ControllerContext = mockedHttpContext.Object;

            var model = new AddImageViewModel();
            model.SelectedImageGalleryId = "testId";
            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            // Act
            var result = controller.Add(mockedFile, model) as ViewResult;

            // Assert
            Assert.IsTrue(mockedGallery.Images.Contains(mockedImage));
            Assert.IsTrue(mockedFile.IsSaveAsCalled);
            Assert.IsFalse(mockedImage.IsConfirmed);

            mockedImageGalleryService.Verify(s => s.Save(), Times.Once);
            mockedImageGalleryService.Verify(s => s.FindById("testId"), Times.Once);

            mockedDateProvider.Verify(d => d.GetDate(), Times.Once);

            mockedLakeService.Verify(l => l.GetAll(), Times.Once);

            mockedDirectoryHelper.Verify(d => d.CreateIfNotExist("Test"), Times.Once);
        }
    }
}
