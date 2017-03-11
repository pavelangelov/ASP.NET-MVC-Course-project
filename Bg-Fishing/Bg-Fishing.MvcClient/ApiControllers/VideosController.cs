using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class VideosController : ApiController
    {
        private IVideoService videoService;

        public VideosController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, "videoService");

            this.videoService = videoService;
        }

        [HttpGet]
        public string Get(string galleryId)
        {
            var videos = this.videoService.GetVideosFromGallery(galleryId);

            var videosArr =  JsonConvert.SerializeObject(videos);

            return videosArr;
        }
    }
}
