using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.LakesControllerTests
{
    [TestFixture]
    public class GetComments_Should
    {
        [Test]
        public void GetCommentsFromService_AndReturnPartilaView()
        {
            // Arrange
            var mockedCollection = new List<CommentModel>
            {
                new CommentModel() { LakeName = "Test", PostedDate = DateTime.UtcNow },
                new CommentModel() { LakeName = "Test 2", PostedDate = DateTime.UtcNow }
            };

            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.GetAllByLakeName(It.IsAny<string>())).Returns(mockedCollection).Verifiable();

            var controller = new LakesController(
                mockedLakeService.Object,
                mockedCommentFactory.Object, 
                mockedDateProvider.Object,
                mockedCommentService.Object);

            // Act
            var result = controller.GetComments(It.IsAny<string>()) as PartialViewResult;
            var model = result.ViewData.Model as IEnumerable<CommentModel>;

            // Assert
            Assert.AreEqual("_CommentsPartial", result.ViewName);
            CollectionAssert.AreEqual(mockedCollection, model);
        }
    }
}
