﻿using System.Web.Mvc;

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
    public class Details_Should
    {
        [Test]
        public void GetLakeFromService_AndRenderDefaultView_WithThisLake()
        {
            // Arrange
            var mockedLake = new Lake();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.FindByName(It.IsAny<string>())).Returns(mockedLake).Verifiable();
            
            var mockedCommentFactory = new Mock<ICommentFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedCommentService = new Mock<ICommentService>();

            var controller = new LakesController(mockedLakeService.Object, mockedCommentFactory.Object, mockedDateProvider.Object, mockedCommentService.Object);

            // Act
            var view = controller.Details(null) as ViewResult;
            var model = view.ViewData.Model as Lake;

            // Assert
            Assert.AreEqual("", view.ViewName);
            Assert.AreEqual(mockedLake, model);
        }
    }
}
