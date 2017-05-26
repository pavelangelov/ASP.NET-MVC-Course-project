using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.PicturesControllerTests
{
    [TestFixture]
    public class GetAdd_Should
    {
        [Test]
        public void GetLakesFromService_AndRetunDefaultView()
        {
            // Arrange
            var mockedLakesCollection = new List<LakeModel>() { new LakeModel() { Name = "Test" } };
            var mockedImageGalleryService = new Mock<IImageGalleryService>();
            var mockedImageFactory = new Mock<IImageFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();
            var mockedLakeService = new Mock<ILakeService>();
            mockedLakeService.Setup(s => s.GetAll()).Returns(mockedLakesCollection).Verifiable();

            var mockedImageGalleryFactory = new Mock<IImageGalleryFactory>();
            var mockedDirectoryHelper = new Mock<IDirectoryHelper>();
            
            var controller = new PicturesController(
                mockedImageGalleryService.Object,
                mockedImageFactory.Object,
                mockedDateProvider.Object,
                mockedLakeService.Object,
                mockedImageGalleryFactory.Object,
                mockedDirectoryHelper.Object);

            // Act
            var result = controller.Add() as ViewResult;
            var model = result.Model as AddImageViewModel;

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(mockedLakesCollection, model.Lakes);

            mockedLakeService.Verify(s => s.GetAll(), Times.Once);
        }
    
    }
}
