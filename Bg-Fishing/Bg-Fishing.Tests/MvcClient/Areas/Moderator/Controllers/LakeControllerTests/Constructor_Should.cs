using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfLakeFactoryIsNull()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeController(
                null, 
                mockedLocationFactory.Object, 
                mockedLakeService.Object, 
                mockedLocationService.Object, 
                mockedFishService.Object)).Message;

            StringAssert.Contains("lakeFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLocationFactoryIsNull()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeController(
                mockedLakeFactory.Object, 
                null,
                mockedLakeService.Object, 
                mockedLocationService.Object, 
                mockedFishService.Object)).Message;

            StringAssert.Contains("locationFactory", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLakeServiceIsNull()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeController(
                mockedLakeFactory.Object, 
                mockedLocationFactory.Object, 
                null, 
                mockedLocationService.Object, 
                mockedFishService.Object)).Message;

            StringAssert.Contains("lakeService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLocationServiceIsNull()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeController(
                mockedLakeFactory.Object, 
                mockedLocationFactory.Object, 
                mockedLakeService.Object, 
                null, 
                mockedFishService.Object)).Message;

            StringAssert.Contains("locationService", message);
        }

        [Test]
        public void ThrowArgumentNullException_IFishServiceIsNull()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeController(
                mockedLakeFactory.Object, 
                mockedLocationFactory.Object,
                mockedLakeService.Object, 
                mockedLocationService.Object,
                null)).Message;

            StringAssert.Contains("fishService", message);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new LakeController(
                mockedLakeFactory.Object, 
                mockedLocationFactory.Object,
                mockedLakeService.Object,
                mockedLocationService.Object, 
                mockedFishService.Object));
        }
    }
}
