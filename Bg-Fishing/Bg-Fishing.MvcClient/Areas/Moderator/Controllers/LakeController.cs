using Bg_Fishing.MvcClient.Areas.Moderator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Controllers
{
    public class LakeController : Controller
    {
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
                // TODO: Add new lake
                return Json(new { status = "success", message = "Язовира е добавен успешно" });
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