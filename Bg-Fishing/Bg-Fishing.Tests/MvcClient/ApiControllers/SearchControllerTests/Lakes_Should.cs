using Bg_Fishing.DTOs.LakeDTOs;
using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.MvcClient.WebApiModels;
using Bg_Fishing.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

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
            var mockedCollection = new List<LakeDTO>()
            {
               new LakeDTO { Name = "First" },
               new LakeDTO { Name = "Second" }
            };

            var model = new SearchModel() { Name = "not match" };
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByLocation(It.IsAny<string>())).Returns(mockedCollection).Verifiable();

            var controller = new SearchController(mockedLakeService.Object);

            // Act
            var result = controller.Lakes(model);
            var content = (result as OkNegotiatedContentResult<IEnumerable<LakeDTO>>).Content;

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<IEnumerable<LakeDTO>>>(result);
            Assert.AreEqual(content, mockedCollection);

            mockedLakeService.Verify(s => s.FindByLocation(It.IsAny<string>()), Times.Once);
        }
    }
}
