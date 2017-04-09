using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.Controllers.LakesControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void GetAllLakesFromService_GroupThem_AndRenderDefaultView()
        {
            // Arrange
            var mockedCollection = new List<LakeModel>
            {
                new LakeModel { Name = "First" },
                new LakeModel { Name = "Second" },
                new LakeModel { Name = "Third" }
            };

            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Returns(mockedCollection).Verifiable();

            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();

            var controller = new LakesController(mockedLakeService.Object, mockedCommentFactory.Object, mockedDateProvider.Object, mockedCommentService.Object);

            // Act
            var view = controller.Index() as ViewResult;
            var model = view.ViewData.Model as IEnumerable<IGrouping<char, LakeModel>>;

            // Assert
            var expectedResult = mockedCollection.GroupBy(l => l.Name.ToLower()[0]);
            Assert.IsTrue(view.ViewName == "");
            CollectionAssert.AreEquivalent(expectedResult, model);

            mockedLakeService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
