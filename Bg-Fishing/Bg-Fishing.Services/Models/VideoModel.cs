using System;
using System.Linq.Expressions;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Services.Models
{
    public class VideoModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime PostedOn { get; set; }

        public static Func<Video, VideoModel> Cast
        {
            get
            {
                return v => new VideoModel()
                {
                    Id = v.Id,
                    Title = v.Title,
                    Url = v.Url,
                    PostedOn = v.PostedOn
                };
            }
        }
    }
}
