using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Models;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.LakesControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void GetLakeFromService_AndRenderDefaultView_WithThisLake()
        {
            // Arrange
            var mockedLake = new Lake();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();

            var mockedCommentService = new Mock<ICommentService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new LakesController(mockedLakeService.Object, mockedCommentService.Object, mockedCommentFactory.Object, mockedDateProvider.Object);

            // Act
            var view = controller.Index(null) as ViewResult;
            var model = view.ViewData.Model as Lake;

            // Assert
            Assert.AreEqual("Index", view.ViewName);
            Assert.AreEqual(mockedLake, model);
        }
    }
}
