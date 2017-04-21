using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class GetEdit_Should
    {
        [Test]
        public void GetLakeFromService_AndReturnDefaultView()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();

            var mockedLake = new Lake() { Name = "Test lake", Info = "Test info" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();

            var mockedLocationService = new Mock<ILocationService>();

            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var result = controller.Edit(It.IsAny<string>()) as ViewResult;
            var model = result.ViewData.Model as EditLakeViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(mockedLake.Name, model.LakeName);
            Assert.AreEqual(mockedLake.Info, model.LakeInfo);
        }
    }
}
