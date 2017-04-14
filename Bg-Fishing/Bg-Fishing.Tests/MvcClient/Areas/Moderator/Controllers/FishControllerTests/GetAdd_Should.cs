using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.FishControllerTests
{
    [TestFixture]
    public class GetAdd_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedFishFactory = new Mock<IFishFactory>();
            var mockedFishService = new Mock<IFishService>();
            var controller = new FishController(mockedFishFactory.Object, mockedFishService.Object);

            // Act
            var result = controller.Add() as ViewResult;
            var model = result.ViewData.Model as AddFishViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsNotNull(model);
        }
    }
}
