using Bg_Fishing.MvcClient.Models.ViewModels.Common;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bg_Fishing.MvcClient.Controllers.Common
{
    public class GalleriesController : Controller
    {
        private IVideoService videoService;

        public GalleriesController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, "videoService");

            this.videoService = videoService;
        }

        // GET: Galleries
        public ActionResult Videos()
        {
            var model = new VideoGalleriesViewModel();

            var galleries = this.videoService.GetAll();
            model.SetGalleries(galleries);

            return View(model);
        }

        [HttpGet]
        public string GetVideos(string galleryId)
        {
            var videos = this.videoService.GetVideosFromGallery(galleryId);

            var videosArr = JsonConvert.SerializeObject(videos);

            return videosArr;
        }
    }
}