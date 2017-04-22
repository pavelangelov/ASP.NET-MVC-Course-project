using System;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class AddFish_Should
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
            var result = controller.AddFish(new UpdateFishViewModel()) as JsonResult;
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
            var result = controller.AddFish(new UpdateFishViewModel() { SelectedFish = selectedFish }) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual(GlobalMessages.AddingFishErrorMessage, dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectSuccessMessage_IfAddingFishNotFailed_AndAddFishToLake()
        {
            // Arrange
            var lakeName = "Lake";
            var expectedMessage = string.Format(GlobalMessages.AddingFishSuccessMessageFormat, lakeName);
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedFish = new Fish();
            var mockedLake = new Lake();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedFish).Verifiable();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            var selectedFish = Enumerable.Repeat<string>("", 1);
            var model = new UpdateFishViewModel() { SelectedFish = selectedFish, SelectedLake = lakeName };

            // Act
            var result = controller.AddFish(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(expectedMessage, dResult.message);
            Assert.IsTrue(mockedLake.Fish.Contains(mockedFish));

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
        }
    }
}
