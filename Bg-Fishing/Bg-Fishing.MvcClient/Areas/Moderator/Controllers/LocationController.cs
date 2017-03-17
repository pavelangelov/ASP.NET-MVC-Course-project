using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LocationController : Controller
    {
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
                // TODO: Add new location
            }

            return View();
        }
    }
}