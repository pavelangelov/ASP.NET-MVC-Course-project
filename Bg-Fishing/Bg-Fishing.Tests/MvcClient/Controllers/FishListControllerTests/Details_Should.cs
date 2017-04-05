using System.Web.Mvc;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.Controllers;
using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Tests.MvcClient.Controllers.FishListControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void CallFindByNameFromService_AndSetFishToViewModel()
        {
            // Arrange
            var mockedFish = new FishModel { Name = "First" };

            var mockedFishService = new Mock<IFishService>();
            mockedFishService.Setup(s => s.GetFishByName(It.IsAny<string>())).Returns(mockedFish).Verifiable();

            var controller = new FishListController(mockedFishService.Object);

            // Act
            var view = controller.Details(null) as ViewResult;
            var model = view.ViewData.Model as FishListViewModel;

            // Assert
            Assert.AreEqual(mockedFish, model.SelectedFish);
            mockedFishService.Verify(s => s.GetFishByName(It.IsAny<string>()), Times.Once);
        }
    }
}
