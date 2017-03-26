using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.CommentControllerTests
{
    [TestFixture]
    public class GetIndex_Should
    {
        [Test]
        public void ReturnDefaultView()
        {
            // Arrange
            var mockedNewsFactory = new Mock<INewsFactory>();
            var mockedNewsService = new Mock<INewsService>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new CommentController(mockedNewsFactory.Object, mockedNewsService.Object, mockedDateProvider.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("", result.ViewName);
        }
    }
}
