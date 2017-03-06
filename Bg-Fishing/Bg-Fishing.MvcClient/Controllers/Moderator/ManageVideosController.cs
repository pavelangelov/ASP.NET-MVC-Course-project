using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Controllers.Moderator
{
    public class ManageVideosController : Controller
    {
        private IVideoService videoService;

        public ManageVideosController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, "videoService");

            this.videoService = videoService;
        }

        [Authorize(Roles = "User,Moderator,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string url, string categoryName)
        {
            if (url == null || url.Length == 0)
            {
                return Json(new { status = "error", message = " Линкът към видеото е невалиден." }); ;
            }

            if (categoryName == null || categoryName.Length == 0)
            {
                return Json(new { status = "error", message = " Не е избрана категория." });
            }

            try
            {
                // Add video to Gallery.
                var video = new Video("Test", url, DateTime.Now);
                this.videoService.AddVideoToGallery(categoryName, video);
                this.videoService.Save();
                return Json(new { status = "success", message = "Видеото е добавено." });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Видеото не може да бъде добавено." });
            }
        }
    }
}