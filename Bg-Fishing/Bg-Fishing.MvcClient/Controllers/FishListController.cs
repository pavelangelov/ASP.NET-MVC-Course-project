using System.Web.Mvc;

using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class FishListController : Controller
    {
        private IFishService fishService;

        public FishListController(IFishService fishService)
        {
            Validator.ValidateForNull(fishService, "fishService");

            this.fishService = fishService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var fishCollection = this.fishService.GetAll();
            var model = new FishListViewModel() { FishCollection = fishCollection };

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(string name)
        {
            var fish = this.fishService.GetFishDTOByName(name);
            var model = new FishListViewModel() { SelectedFish = fish };

            return View(model);
        }
    }
}