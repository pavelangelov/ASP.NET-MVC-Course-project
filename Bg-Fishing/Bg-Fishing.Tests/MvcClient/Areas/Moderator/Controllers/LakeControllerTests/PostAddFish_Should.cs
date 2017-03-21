using System;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class PostAddFish_Should
    {
        [Test]
        public void ReturnJsonWithAllModelErrors_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);
            controller.ModelState.AddModelError("Name", "Test error!");

            // Act
            var result = controller.AddFish(new AddFishViewModel()) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            StringAssert.Contains("Test error!", dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
            mockedLakeService.Verify(s => s.Save(), Times.Never);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectErrorMessage_IfAddingFishFailed()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Throws<Exception>();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var selectedFish = Enumerable.Empty<string>();
            var result = controller.AddFish(new AddFishViewModel() { SelectedFish = selectedFish }) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual("Възникна грешка при добавянето на на избраните риби.", dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectSuccessMessage_IfAddingFishNotFailed()
        {
            // Arrange
            var lakeName = "Lake";
            var expectedMessage = string.Format("Рибата е добавена във {0}.", lakeName);
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            var selectedFish = Enumerable.Empty<string>();
            var model = new AddFishViewModel() { SelectedFish = selectedFish, SelectedLake = lakeName };

            // Act
            var result = controller.AddFish(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(expectedMessage, dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }
    }
}
