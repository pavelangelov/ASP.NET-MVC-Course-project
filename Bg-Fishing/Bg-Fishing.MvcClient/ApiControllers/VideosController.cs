using Bg_Fishing.DTOs;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class VideosController : ApiController
    {
        private IVideoService videoService;

        public VideosController(IVideoService videoService)
        {
            Validator.ValidateForNull(videoService, paramName: "videoService");

            this.videoService = videoService;
        }

        public IEnumerable<VideoDTO> GetVideos(string galleryId)
        {
            var videos = this.videoService.GetVideosFromGallery(galleryId);

            if (videos == null)
            {
                return Enumerable.Empty<VideoDTO>();
            }

            foreach (var video in videos)
            {
                video.Url = VideoHelper.FixUrl(video.Url);
            }

            return videos;
        }
    }
}
