using System.Collections.Generic;
using System.Web.Http.Results;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.MvcClient.WebApiModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.SearchControllerTests
{
    [TestFixture]
    public class Lakes_Should
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("aa")]
        public void ReturnNotFound_IfNameIsNotValid(string invalidName)
        {
            // Arrange
            var model = new SearchModel() { Name = invalidName };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByLocation(It.IsAny<string>())).Verifiable();

            var controller = new SearchController(mockedLakeService.Object);

            // Act
            var result = controller.Lakes(model);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            mockedLakeService.Verify(s => s.FindByLocation(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnNotFound_IfNameNotMatch()
        {
            // Arrange
            var model = new SearchModel() { Name = "not match" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByLocation(It.IsAny<string>())).Verifiable();

            var controller = new SearchController(mockedLakeService.Object);

            // Act
            var result = controller.Lakes(model);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            mockedLakeService.Verify(s => s.FindByLocation(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_IfNameMatch()
        {
            // Arrange
            var mockedCollection = new List<LakeModel>()
            {
               new LakeModel { Name = "First" },
               new LakeModel { Name = "Second" }
            };

            var model = new SearchModel() { Name = "not match" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByLocation(It.IsAny<string>())).Returns(mockedCollection).Verifiable();

            var controller = new SearchController(mockedLakeService.Object);

            // Act
            var result = controller.Lakes(model);
            var content = (result as OkNegotiatedContentResult<IEnumerable<LakeModel>>).Content;

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<IEnumerable<LakeModel>>>(result);
            Assert.AreEqual(content, mockedCollection);

            mockedLakeService.Verify(s => s.FindByLocation(It.IsAny<string>()), Times.Once);
        }
    }
}
