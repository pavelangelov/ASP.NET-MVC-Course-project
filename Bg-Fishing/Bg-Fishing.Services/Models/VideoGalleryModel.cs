using System;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Services.Models
{
    public class VideoGalleryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public static Func<VideoGallery, VideoGalleryModel> Cast
        {
            get
            {
                return g => new VideoGalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name
                };
            }
        }
    }
}
