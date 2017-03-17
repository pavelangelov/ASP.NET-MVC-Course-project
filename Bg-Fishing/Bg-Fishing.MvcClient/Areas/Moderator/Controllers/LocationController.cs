using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LocationController : Controller
    {
        private ILocationFactory locationFactory;

        public LocationController(ILocationFactory locationFactory)
        {
            Validator.ValidateForNull(locationFactory, "locationFactory");

            this.locationFactory = locationFactory;
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
                var location = this.locationFactory.CreateLocation(model.Latitude, model.Longitude, model.Name, model.Info);
                // TODO: Add new location
            }

            return View();
        }
    }
}