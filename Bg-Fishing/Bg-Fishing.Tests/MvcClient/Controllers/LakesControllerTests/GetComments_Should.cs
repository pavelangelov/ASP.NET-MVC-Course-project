using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;
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
            var mockedCollection = this.GetCommentModelCollection();

            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();
            mockedCommentService.Setup(s => s.GetCommentsCount(It.IsAny<string>())).Returns(mockedCollection.Count).Verifiable();

            var controller = new LakesController(
                mockedLakeService.Object,
                mockedCommentFactory.Object, 
                mockedDateProvider.Object,
                mockedCommentService.Object);
            var lakeName = "Test";
            var expectedSkip = 0;

            // Act
            var result = controller.GetComments(lakeName) as PartialViewResult;
            var model = result.ViewData.Model as GetCommentsViewModel;

            // Assert
            Assert.AreEqual("_CommentsPartial", result.ViewName);
            Assert.IsFalse(model.HasPrev);
            Assert.IsTrue(model.PrevPage == 0);
            CollectionAssert.AreEqual(mockedCollection, model.Comments);

            mockedCommentService.Verify(s => s.GetCommentsByLakeName(lakeName, expectedSkip, Constants.ShowedComments), Times.Once);
            mockedCommentService.Verify(s => s.GetCommentsCount(lakeName), Times.Once);
        }

        [Test]
        public void GetCommentsFromService_SetNextPageToModel_IfHaveMoreComments_AndReturnPartilaView()
        {
            // Arrange
            var mockedCollection = this.GetCommentModelCollection();

            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection.GetRange(0, Constants.ShowedComments)).Verifiable();
            mockedCommentService.Setup(s => s.GetCommentsCount(It.IsAny<string>())).Returns(mockedCollection.Count).Verifiable();

            var controller = new LakesController(
                mockedLakeService.Object,
                mockedCommentFactory.Object,
                mockedDateProvider.Object,
                mockedCommentService.Object);

            // Act
            var result = controller.GetComments(It.IsAny<string>()) as PartialViewResult;
            var model = result.ViewData.Model as GetCommentsViewModel;

            // Assert
            Assert.AreEqual("_CommentsPartial", result.ViewName);
            Assert.IsTrue(model.HasNext);
            Assert.IsTrue(model.NextPage == 1);

            mockedCommentService.Verify(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockedCommentService.Verify(s => s.GetCommentsCount(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetCommentsFromService_SetPrevPageToModel_IfHavePreviousComments_AndReturnPartilaView()
        {
            // Arrange
            var mockedCollection = this.GetCommentModelCollection();

            var mockedLakeService = new Mock<ILakeService>();
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection.GetRange(1, Constants.ShowedComments)).Verifiable();
            mockedCommentService.Setup(s => s.GetCommentsCount(It.IsAny<string>())).Returns(mockedCollection.Count).Verifiable();

            var controller = new LakesController(
                mockedLakeService.Object,
                mockedCommentFactory.Object,
                mockedDateProvider.Object,
                mockedCommentService.Object);

            // Act
            var result = controller.GetComments(It.IsAny<string>(), 1) as PartialViewResult;
            var model = result.ViewData.Model as GetCommentsViewModel;

            // Assert
            Assert.AreEqual("_CommentsPartial", result.ViewName);
            Assert.IsTrue(model.HasPrev);
            Assert.IsTrue(model.PrevPage == 0);

            mockedCommentService.Verify(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockedCommentService.Verify(s => s.GetCommentsCount(It.IsAny<string>()), Times.Once);
        }

        private List<CommentModel> GetCommentModelCollection()
        {
            return new List<CommentModel>
            {
                new CommentModel() { LakeName = "Test", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() },
                new CommentModel() { LakeName = "Test 2", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() },
                new CommentModel() { LakeName = "Test", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() },
                new CommentModel() { LakeName = "Test 2", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() },
                new CommentModel() { LakeName = "Test", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() },
                new CommentModel() { LakeName = "Test 2", PostedDate = DateTime.UtcNow, Comments = new List<InnerCommentModel>() }
            };
        }
    }
}
