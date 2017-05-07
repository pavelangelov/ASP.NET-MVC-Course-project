using System;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Services.Models
{
    public class ImageModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Info { get; set; }

        public DateTime? Date { get; set; }

        public static Func<Image, ImageModel> Cast
        {
            get
            {
                return i => new ImageModel()
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    Info = i.Info,
                    Date = i.Date
                };
            }
        }
    }
}
