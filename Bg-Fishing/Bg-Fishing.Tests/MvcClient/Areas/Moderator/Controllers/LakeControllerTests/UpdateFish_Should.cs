using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class UpdateFish_Should
    {
        [Test]
        public void GetFishAndLakesFromServices_AndCallDefault()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.GetAll()).Verifiable();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var view = controller.UpdateFish() as ViewResult;

            // Assert
            Assert.IsNotNull(view.ViewData.Model);
            Assert.AreEqual("", view.ViewName);

            mockedLakeService.Verify(s => s.GetAll(), Times.Once);
            mockedFishService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
