﻿using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.CommentsApiControllerTests
{
    [TestFixture]
    public class Comments_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var date = DateTime.UtcNow;
            var mockedCollection = new List<CommentModel>()
            {
                new CommentModel {LakeName = "First", PostedDate = date.AddHours(1) },
                new CommentModel {LakeName = "Second", PostedDate = date.AddHours(2) },
                new CommentModel {LakeName = "Third", PostedDate = date.AddHours(3) }
            };

            var mockedCommentService = new Mock<ICommentService>();
            mockedCommentService.Setup(s => s.GetCommentsByLakeName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(mockedCollection).Verifiable();

            var mockedInnerCommentFactory = new Mock<IInnerCommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var controller = new CommentsApiController(mockedCommentService.Object, mockedInnerCommentFactory.Object, mockedDateProvider.Object);

            // Act
            var result = controller.Comments(null, It.IsAny<int>());
            var expectedResult = mockedCollection.OrderByDescending(c => c.PostedDate).ToList();

            // Assert
            var index = 0;
            foreach (var comment in result)
            {
                Assert.AreEqual(expectedResult[index].LakeName, comment.LakeName);
                Assert.AreEqual(expectedResult[index].PostedDate, comment.PostedDate);
                index++;
            }
        }
    }
}
