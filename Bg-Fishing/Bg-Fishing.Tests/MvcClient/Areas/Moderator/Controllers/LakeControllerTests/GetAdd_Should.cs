using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class GetAdd_Should
    {
        [Test]
        public void ReturnCorrectView()
        {
            // Arrange
            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();
            var mockedFishService = new Mock<IFishService>();

            var controller = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object, mockedFishService.Object);

            // Act
            var view = controller.Add() as ViewResult;

            // Assert
            Assert.IsTrue(view.ViewName == ""); // If ViewName isn`t empty string, then controller retuns not his default View
        }
    }
}
