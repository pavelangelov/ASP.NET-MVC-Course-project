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
        private ILakeFactory lakeFactory;
        private ILocationFactory locationFactory;
        private ILakeService lakeService;
        private ILocationService locationService;

        public LakeController(
            ILakeFactory lakeFactory, 
            ILocationFactory locationFactory, 
            ILakeService lakeService, 
            ILocationService locationService)
        {
            Validator.ValidateForNull(lakeFactory, paramName: "lakeFactory");
            Validator.ValidateForNull(locationFactory, paramName: "locationFactory");
            Validator.ValidateForNull(lakeService, paramName: "lakeService");
            Validator.ValidateForNull(locationService, paramName: "locationService");

            this.lakeFactory = lakeFactory;
            this.locationFactory = locationFactory;
            this.lakeService = lakeService;
            this.locationService = locationService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
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
    }
}