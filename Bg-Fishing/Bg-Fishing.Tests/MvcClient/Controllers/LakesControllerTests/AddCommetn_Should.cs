using System;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;
using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Models;

namespace Bg_Fishing.Tests.MvcClient.Controllers.LakesControllerTests
{
    [TestFixture]
    public class AddCommetn_Should
    {
        [Test]
        public void ReturnJsonWithCorrectErrorMessage_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedCommentService = new Mock<ICommentService>();

            var mockedCommentFactory = new Mock<ICommentFactory>();
            mockedCommentFactory.Setup(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(p => p.GetDate()).Verifiable();

            var controller = new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object);
            controller.ModelState.AddModelError("Content", "Error");

            // Act
            var result = controller.AddComment(null) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual(GlobalMessages.AddCOmentInvalidModelStateErrorMessage, dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
            mockedLakeService.Verify(s => s.Save(), Times.Never);

            mockedCommentFactory.Verify(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);

            mockedDateProvider.Verify(p => p.GetDate(), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectErrorMessage_IfAddingCommentFailed()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Throws<Exception>();

            var mockedCommentService = new Mock<ICommentService>();

            var mockedCommentFactory = new Mock<ICommentFactory>();
            mockedCommentFactory.Setup(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(p => p.GetDate()).Verifiable();

            var mockedContext = new Mock<ControllerContext>();
            mockedContext.Setup(c => c.HttpContext.User.Identity.Name).Returns("test");

            var controller = new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object);
            controller.ControllerContext = mockedContext.Object;

            var model = new AddCommentViewModel() { Content = "test content", LakeName = "test lake" };

            // Act
            var result = controller.AddComment(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual(GlobalMessages.AddCommentErrorMessage, dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);

            mockedCommentFactory.Verify(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedDateProvider.Verify(p => p.GetDate(), Times.Once);
        }

        [Test]
        public void ReturnJsonWithCorrectSuccessMessage_IfAddingCommentNotFailed()
        {
            // Arrange
            var mockedLake = new Lake();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedCommentService = new Mock<ICommentService>();

            var mockedCommentFactory = new Mock<ICommentFactory>();
            mockedCommentFactory.Setup(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Verifiable();

            var mockedDateProvider = new Mock<IDateProvider>();
            mockedDateProvider.Setup(p => p.GetDate()).Verifiable();

            var mockedContext = new Mock<ControllerContext>();
            mockedContext.Setup(c => c.HttpContext.User.Identity.Name).Returns("test");

            var controller = new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object);
            controller.ControllerContext = mockedContext.Object;

            var model = new AddCommentViewModel() { Content = "test content", LakeName = "test lake" };

            // Act
            var result = controller.AddComment(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(GlobalMessages.AddCommentSuccessMessage, dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);

            mockedCommentFactory.Verify(f => f.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);

            mockedDateProvider.Verify(p => p.GetDate(), Times.Once);
        }
    }
}
