using System;
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
    public class PostEdit_Should
    {
        [Test]
        public void SetErrorMessage_InTempData_IfEditFailed()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLake = new Lake() { Name = "Test lake", Info = "Test info" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Throws<Exception>();

            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);
            var model = new EditLakeViewModel() { LakeName = "Test name", OldName = "Test name", LakeInfo = "Test info" };

            // Act
            var result = controller.Edit(model, model.OldName) as ViewResult;

            // Assert
            Assert.AreEqual(GlobalMessages.EditLakeFailMessage, result.TempData[GlobalMessages.FailKey]);
        }

        [Test]
        public void SetNewValuesToLake_AndSetSuccessMessage_InTempData_IfEditNotFailed()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLake = new Lake() { Name = "Test lake", Info = "Test info" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);
            var model = new EditLakeViewModel() { LakeName = "Test name", OldName = "Test name", LakeInfo = "Test info" };

            // Act
            var result = controller.Edit(model, model.OldName) as ViewResult;

            // Assert
            Assert.AreEqual(model.LakeName, mockedLake.Name);
            Assert.AreEqual(model.LakeInfo, mockedLake.Info);
            Assert.AreEqual(GlobalMessages.EditLakeSuccessMessage, result.TempData[GlobalMessages.SuccessEditKey]);

            mockedLakeService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);
        }
    }
}
