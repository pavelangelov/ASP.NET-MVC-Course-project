using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Controllers.Moderator
{
    public class ManageVideosController : Controller
    {
        // GET: ManageVideos
        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string url)
        {
            if (url == null || url.Length == 0)
            {
                return Json(new { status = "error", message = "Видеото не може да бъде добавено." }); ;
            }

            try
            {
                // Add video to Gallery.
                return Json(new { status = "success", message = "Видеото е добавено." });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Видеото не може да бъде добавено." });
            }
        }
    }
}