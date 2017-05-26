using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class VideosApiController : ApiController
    {
        private IVideoService videoService;

        public VideosApiController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, paramName: "videoService");

            this.videoService = videoService;
        }

        public IEnumerable<VideoModel> GetVideos(string galleryId)
        {
            var videos = this.videoService.GetVideosFromGallery(galleryId);

            if (videos == null)
            {
                return Enumerable.Empty<VideoModel>();
            }

            return videos;
        }
    }
}
