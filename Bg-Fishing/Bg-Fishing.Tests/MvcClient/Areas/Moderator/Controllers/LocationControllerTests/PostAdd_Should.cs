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

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LocationControllerTests
{
    [TestFixture]
    public class PostAdd_Should
    {
        [Test]
        public void ReturnJsonWihtCorrectErrorMessage_IfModelStateIsNotValid()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.Add(It.IsAny<Location>())).Verifiable();

            var controller = new LocationController(mockedLocationFactory.Object, mockedLocationService.Object);
            var model = new LocationViewModel();
            controller.ModelState.AddModelError("Name", "Error");
            
            // Act
            var result = controller.Add(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.InvalidLocationModelErrorMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()), Times.Never);

            mockedLocationService.Verify(s => s.Add(It.IsAny<Location>()), Times.Never);
            mockedLocationService.Verify(s => s.Save(), Times.Never);
        }

        [Test]
        public void ReturnJsonWihtCorrectErrorMessage_IfAddingLocationFail()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.Add(It.IsAny<Location>())).Verifiable();
            mockedLocationService.Setup(s => s.Save()).Throws<Exception>();

            var controller = new LocationController(mockedLocationFactory.Object, mockedLocationService.Object);
            var model = new LocationViewModel() { Name = "Name", Latitude = 1, Longitude = 1 } ;

            // Act
            var result = controller.Add(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.AddLocationErrorMessage, dResult.message);
            Assert.AreEqual("error", dResult.status);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockedLocationService.Verify(s => s.Add(It.IsAny<Location>()), Times.Once);
        }

        [Test]
        public void ReturnJsonWihtCorrectSuccessMessage_IfAddingLocationComplete()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            mockedLocationFactory.Setup(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();
            mockedLocationService.Setup(s => s.Add(It.IsAny<Location>())).Verifiable();
            mockedLocationService.Setup(s => s.Save()).Verifiable();

            var controller = new LocationController(mockedLocationFactory.Object, mockedLocationService.Object);
            var model = new LocationViewModel() { Name = "Name", Latitude = 1, Longitude = 1 };

            // Act
            var result = controller.Add(model) as JsonResult;
            dynamic dResult = result.Data;

            // Assert
            Assert.AreEqual(GlobalMessages.AddLocationSuccessMessage, dResult.message);
            Assert.AreEqual("success", dResult.status);

            mockedLocationFactory.Verify(f => f.CreateLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockedLocationService.Verify(s => s.Add(It.IsAny<Location>()), Times.Once);
            mockedLocationService.Verify(s => s.Save(), Times.Once);
        }
    }
}
