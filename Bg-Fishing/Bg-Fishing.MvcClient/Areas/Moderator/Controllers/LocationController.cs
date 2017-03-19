using System;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LocationController : ModeratorBaseController
    {
        private ILocationFactory locationFactory;
        private ILocationService locationService;

        public LocationController(ILocationFactory locationFactory, ILocationService locationService)
        {
            Validator.ValidateForNull(locationFactory, "locationFactory");
            Validator.ValidateForNull(locationService, "locationService");

            this.locationFactory = locationFactory;
            this.locationService = locationService;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var location = this.locationFactory.CreateLocation(model.Latitude, model.Longitude, model.LocationName, model.Info);
                    this.locationService.Add(location);
                    this.locationService.Save();

                    return Json(new { status = "success", message = GlobalMessages.AddLocationSuccessMessage });
                }
                catch (Exception)
                {
                    return Json(new { status = "error", message = GlobalMessages.AddLocationErrorMessage });
                }
            }

            return Json(new { status = "error", message = GlobalMessages.InvalidLocationModelErrorMessage });
        }
    }
}