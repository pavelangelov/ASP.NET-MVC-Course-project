using Bg_Fishing.MvcClient.Models.ViewModels;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var fish = this.fishService.FindByName(name);
            var model = new FishListViewModel() { SelectedFish = fish };

            return View(model);
        }
    }
}