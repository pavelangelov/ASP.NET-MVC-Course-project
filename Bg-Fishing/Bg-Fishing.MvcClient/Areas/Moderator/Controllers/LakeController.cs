using System;
using System.Linq;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LakeController : ModeratorBaseController
    {
        public const string EditLakeSuccessMessage = "Промените са направени!";
        public const string EditLakeFailMessage = "Промените не могът да бъдат направени в момента!";
        public const string SuccessEditKey = "LakeEditSuccess";
        public const string FailKey = "LakeEditFail";

        private ILakeFactory lakeFactory;
        private ILocationFactory locationFactory;
        private ILakeService lakeService;
        private ILocationService locationService;
        private IFishService fishService;

        public LakeController(
            ILakeFactory lakeFactory,
            ILocationFactory locationFactory,
            ILakeService lakeService,
            ILocationService locationService,
            IFishService fishService)
        {
            Validator.ValidateForNull(lakeFactory, paramName: "lakeFactory");
            Validator.ValidateForNull(locationFactory, paramName: "locationFactory");
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(locationService, paramName: "locationService");
            Validator.ValidateForNull(fishService, paramName: "fishService");

            this.lakeFactory = lakeFactory;
            this.locationFactory = locationFactory;
            this.lakeService = lakeService;
            this.locationService = locationService;
            this.fishService = fishService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(LakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var location = this.locationService.FindByName(model.LocationName);
                    if (location == null)
                    {
                        location = this.locationFactory.CreateLocation(model.Latitude, model.Longitude, model.LocationName);
                    }

                    var lake = this.lakeFactory.CreateLake(model.Name, location, model.Info);
                    this.lakeService.Add(lake);
                    this.lakeService.Save();

                    return Json(new { status = "success", message = GlobalMessages.AddLakeSuccessMessage });
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = GlobalMessages.AddLakeErrorMessage });
                }

            }
            else
            {
                var errors = string.Join("<br/>", ModelState.Values
                                                        .SelectMany(v => v.Errors
                                                                          .Select(e => e.ErrorMessage)));
                return Json(new { status = "error", message = errors });
            }
        }

        [HttpGet]
        public ActionResult UpdateFish()
        {
            var model = new UpdateFishViewModel();
            var fish = this.fishService.GetAll();
            model.Fish = fish;

            var lakes = this.lakeService.GetAll();
            model.Lakes = lakes;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFish(UpdateFishViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var lake = this.lakeService.FindByName(model.SelectedLake);
                    foreach (var fishName in model.SelectedFish)
                    {
                        var fish = this.fishService.FindByName(fishName);
                        lake.Fish.Add(fish);
                    }

                    this.lakeService.Save();
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = "Възникна грешка при добавянето на на избраните риби." });
                }

                return Json(new { status = "success", message = string.Format("Рибата е добавена във {0}.", model.SelectedLake) });
            }
            else
            {
                var errors = string.Join("<br/>", ModelState.Values
                                                        .SelectMany(v => v.Errors
                                                                          .Select(e => e.ErrorMessage)));
                return Json(new { status = "error", message = errors });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RemoveFish(UpdateFishViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var lake = this.lakeService.FindByName(model.SelectedLake);
                    foreach (var fishName in model.SelectedFish)
                    {
                        var fish = this.fishService.FindByName(fishName);
                        lake.Fish.Remove(fish);
                    }

                    this.lakeService.Save();
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = "Възникна грешка при премахването на избраните риби." });
                }

                return Json(new { status = "success", message = string.Format("Рибата е премахната успешно") });
            }
            else
            {
                var errors = string.Join("<br/>", ModelState.Values
                                                        .SelectMany(v => v.Errors
                                                                          .Select(e => e.ErrorMessage)));
                return Json(new { status = "error", message = errors });
            }
        }

        [HttpGet]
        public ActionResult Edit(string name)
        {
            var lake = this.lakeService.FindByName(name);
            var model = new EditLakeViewModel();

            model.OldName = lake.Name;
            model.LakeName = lake.Name;
            model.LakeInfo = lake.Info;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditLakeViewModel model, string oldName)
        {
            if (ModelState.IsValid)
            {
                var lake = this.lakeService.FindByName(oldName);

                try
                {
                    lake.Name = model.LakeName;
                    lake.Info = model.LakeInfo;

                    this.lakeService.Save();
                    TempData[SuccessEditKey] = EditLakeSuccessMessage;
                }
                catch (Exception )
                {
                    TempData[FailKey] = EditLakeFailMessage;
                }
            }

            return View(model);
        }
    }
}