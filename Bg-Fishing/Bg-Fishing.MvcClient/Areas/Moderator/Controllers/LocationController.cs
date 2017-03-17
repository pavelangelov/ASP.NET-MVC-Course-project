using System;
using System.Web.Mvc;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LocationController : Controller
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
                    var location = this.locationFactory.CreateLocation(model.Latitude, model.Longitude, model.Name, model.Info);
                    this.locationService.Add(location);
                    this.locationService.Save();
                }
                catch (Exception)
                {
                    // TODO: Return friendly error message to user
                    throw;
                }
            }

            return View();
        }
    }
}