using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.DTOs.FishDTOs;
using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Controllers.FishListControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallGetAllFromService_AndSetFishToViewModel()
        {
            // Arrange
            var mockedCollection = new List<FishDTO>()
            {
                new FishDTO { Name = "First" },
                new FishDTO { Name = "Second" },
                new FishDTO { Name = "Third" }
            };

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.GetAll()).Returns(mockedCollection).Verifiable();

            var controller = new FishListController(mockedFishService.Object);

            // Act
            var view = controller.Index() as ViewResult;
            var model = view.ViewData.Model as FishListViewModel;

            // Assert
            CollectionAssert.AreEqual(mockedCollection, model.FishCollection);
            mockedFishService.Verify(s => s.GetAll(), Times.Once);
        }
    }
}
