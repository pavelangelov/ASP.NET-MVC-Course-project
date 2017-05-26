using System.Web.Mvc;

using Newtonsoft.Json;

using Bg_Fishing.MvcClient.Models.ViewModels.Common;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Controllers
{
    public class VideosController : Controller
    {
        private IVideoService videoService;

        public VideosController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, "videoService");

            this.videoService = videoService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var model = new VideoGalleriesViewModel();

            var galleries = this.videoService.GetAll();
            model.SetGalleries(galleries);

            return View(model);
        }

        [HttpGet]
        public string GetAll(string galleryId)
        {
            var videos = this.videoService.GetVideosFromGallery(galleryId);
            
            var videosArr = JsonConvert.SerializeObject(videos);

            return videosArr;
        }

        [HttpGet]
        public ActionResult Watch(string id)
        {
            var video = this.videoService.GetVideoById(id);

            return View(video);
        }
    }
}