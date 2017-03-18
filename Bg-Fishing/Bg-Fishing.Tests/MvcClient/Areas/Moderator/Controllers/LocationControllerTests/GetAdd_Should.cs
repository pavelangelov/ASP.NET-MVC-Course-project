using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LocationControllerTests
{
    [TestFixture]
    public class GetAdd_Should
    {
        [Test]
        public void RetunrView_WithNameAdd()
        {
            // Arrange
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLocationService = new Mock<ILocationService>();

            var controller = new LocationController(mockedLocationFactory.Object, mockedLocationService.Object);

            // Act
            var view = controller.Add() as ViewResult;

            // Assert
            Assert.IsTrue(view.ViewName == ""); // If ViewName isn`t empty string, then controller retuns not his default View
        }
    }
}
