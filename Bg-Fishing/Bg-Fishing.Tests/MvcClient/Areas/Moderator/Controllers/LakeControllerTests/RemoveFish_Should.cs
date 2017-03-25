using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class RemoveFish_Should
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
            var result = controller.RemoveFish(new UpdateFishViewModel()) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            StringAssert.Contains("Test error!", dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
            mockedLakeService.Verify(s => s.Save(), Times.Never);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectErrorMessage_IfRemovingFishFailed()
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
            var result = controller.RemoveFish(new UpdateFishViewModel() { SelectedFish = selectedFish }) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual("Възникна грешка при премахването на избраните риби.", dResult.message);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectSuccessMessage_IfRemovingFishNotFailed_AndRemoveFishFromLake()
        {
            // Arrange
            var lakeName = "Lake";
            var expectedMessage = "Рибата е премахната успешно";
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedFish = new Fish();
            var mockedLake = new Lake();
            mockedLake.Fish.Add(mockedFish);
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
            var result = controller.RemoveFish(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(expectedMessage, dResult.message);
            Assert.IsFalse(mockedLake.Fish.Contains(mockedFish));

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);

            mockedFishService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);

        }
    }
}
