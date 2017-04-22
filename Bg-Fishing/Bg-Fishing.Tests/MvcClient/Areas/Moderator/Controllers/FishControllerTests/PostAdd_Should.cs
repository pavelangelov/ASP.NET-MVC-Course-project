using System;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Tests.MvcClient.Mocks;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.FishControllerTests
{
    [TestFixture]
    public class PostAdd_Should
    {
        [Test]
        public void ReturnCorrectErrorMessage_IfFileIsNull()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.Add(It.IsAny<Fish>())).Verifiable();
            mockedFishService.Setup(s => s.Save()).Verifiable();

            var mockedFishFactory = new Mock<IFishFactory>();
            mockedFishFactory.Setup(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var controller = new FishController(mockedFishFactory.Object, mockedFishService.Object);
            var model = new AddFishViewModel() { FishName = "Test", FishType = FishType.SeaFish, Info = "Test" };

            // Act
            var result = controller.Add(model, null) as ViewResult;

            // Assert
            ModelState modelError;
            result.ViewData.ModelState.TryGetValue("", out modelError);

            Assert.IsNull(result.TempData[GlobalMessages.FishAddedSuccessKey]);
            Assert.AreEqual(GlobalMessages.FishAddingErrorMessage, modelError.Errors.First().ErrorMessage);

            mockedFishService.Verify(s => s.Add(It.IsAny<Fish>()), Times.Never);
            mockedFishService.Verify(s => s.Save(), Times.Never);

            mockedFishFactory.Verify(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnCorrectErrorMessage_IfFileLengthIsNotValid()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.Add(It.IsAny<Fish>())).Verifiable();
            mockedFishService.Setup(s => s.Save()).Verifiable();

            var mockedFishFactory = new Mock<IFishFactory>();
            mockedFishFactory.Setup(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize + 1);

            var controller = new FishController(mockedFishFactory.Object, mockedFishService.Object);
            var model = new AddFishViewModel() { FishName = "Test", FishType = FishType.SeaFish, Info = "Test" };

            // Act
            var result = controller.Add(model, mockedFile) as ViewResult;

            // Assert
            ModelState modelError;
            result.ViewData.ModelState.TryGetValue("", out modelError);

            Assert.IsNull(result.TempData[GlobalMessages.FishAddedSuccessKey]);
            Assert.AreEqual(GlobalMessages.FishAddingErrorMessage, modelError.Errors.First().ErrorMessage);

            mockedFishService.Verify(s => s.Add(It.IsAny<Fish>()), Times.Never);
            mockedFishService.Verify(s => s.Save(), Times.Never);

            mockedFishFactory.Verify(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnCorrectErrorMessage_IfAddingFishFailed()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.Add(It.IsAny<Fish>())).Verifiable();
            mockedFishService.Setup(s => s.Save()).Throws<Exception>();

            var mockedFishFactory = new Mock<IFishFactory>();
            mockedFishFactory.Setup(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);
            
            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");

            var controller = new FishController(mockedFishFactory.Object, mockedFishService.Object);
            controller.ControllerContext = mockedHttpContext.Object;
            var model = new AddFishViewModel() { FishName = "Test", FishType = FishType.SeaFish, Info = "Test" };

            // Act
            var result = controller.Add(model, mockedFile) as ViewResult;

            // Assert
            ModelState modelError;
            result.ViewData.ModelState.TryGetValue("", out modelError);

            Assert.IsNull(result.TempData[GlobalMessages.FishAddedSuccessKey]);
            Assert.AreEqual(GlobalMessages.FishAddingFailMessage, modelError.Errors.First().ErrorMessage);

            mockedFishService.Verify(s => s.Add(It.IsAny<Fish>()), Times.Once);

            mockedFishFactory.Verify(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView_AndSetSuccessMessageToTempData_IfAddingFishNotFailed()
        {
            // Arrange
            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.Add(It.IsAny<Fish>())).Verifiable();
            mockedFishService.Setup(s => s.Save()).Verifiable();

            var mockedFishFactory = new Mock<IFishFactory>();
            mockedFishFactory.Setup(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockedFile = new MockHttpPostedFileBase();
            mockedFile.SetContentLength(Constants.ImageMaxSize);

            var mockedHttpContext = new Mock<ControllerContext>();
            mockedHttpContext.Setup(c => c.HttpContext.Server.MapPath(It.IsAny<string>())).Returns("Test");

            var controller = new FishController(mockedFishFactory.Object, mockedFishService.Object);
            controller.ControllerContext = mockedHttpContext.Object;
            var model = new AddFishViewModel() { FishName = "Test", FishType = FishType.SeaFish, Info = "Test" };

            // Act
            var result = controller.Add(model, mockedFile) as ViewResult;

            // Assert
            Assert.IsNotNull(result.TempData[GlobalMessages.FishAddedSuccessKey]);
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(model, result.ViewData.Model);

            mockedFishService.Verify(s => s.Add(It.IsAny<Fish>()), Times.Once);
            mockedFishService.Verify(s => s.Save(), Times.Once);

            mockedFishFactory.Verify(f => f.CreateFish(It.IsAny<string>(), It.IsAny<FishType>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
