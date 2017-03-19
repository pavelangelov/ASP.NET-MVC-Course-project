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
    public class PostAdd_Should
    {
        [Test]
        public void ReturnJsonWithAllModelErrors_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            mockedLakeFactory.Setup(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>())).Verifiable();

            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.Add(It.IsAny<Lake>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();
            
            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);
            controller.ModelState.AddModelError("Name", "Test error!");

            // Act
            var result = controller.Add(new LakeViewModel()) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            StringAssert.Contains("Test error!", dResult.message);

            mockedLakeFactory.Verify(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>()), Times.Never);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Never);

            mockedLakeService.Verify(s => s.Add(It.IsAny<Lake>()), Times.Never);
            mockedLakeService.Verify(s => s.Save(), Times.Never);

            mockedLocationService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnJsonWithCorrectErrorMessage_IfAddingLakeFailed()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            mockedLakeFactory.Setup(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>())).Verifiable();

            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.Add(It.IsAny<Lake>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Throws<Exception>();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var result = controller.Add(new LakeViewModel()) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("error", dResult.status);
            Assert.AreEqual(GlobalMessages.AddLakeErrorMessage, dResult.message);

            mockedLakeFactory.Verify(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>()), Times.Once);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);

            mockedLakeService.Verify(s => s.Add(It.IsAny<Lake>()), Times.Once);

            mockedLocationService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnJsonWithCorrectSuccessMessage_IfAddingLakeNotFailed()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            mockedLakeFactory.Setup(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>())).Verifiable();

            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).Verifiable();

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.Add(It.IsAny<Lake>())).Verifiable();
            mockedLakeService.Setup(s => s.Save()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.FindByName(It.IsAny<string>())).Verifiable();

            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var result = controller.Add(new LakeViewModel()) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual("success", dResult.status);
            Assert.AreEqual(GlobalMessages.AddLakeSuccessMessage, dResult.message);

            mockedLakeFactory.Verify(f => f.CreateLake(It.IsAny<string>(), It.IsAny<Location>(), It.IsAny<string>()), Times.Once);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);

            mockedLakeService.Verify(s => s.Add(It.IsAny<Lake>()), Times.Once);
            mockedLakeService.Verify(s => s.Save(), Times.Once);

            mockedLocationService.Verify(s => s.FindByName(It.IsAny<string>()), Times.Once);
        }
    }
}
