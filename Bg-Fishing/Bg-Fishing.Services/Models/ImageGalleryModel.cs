using System;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Services.Models
{
    public class ImageGalleryModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int ImagesCount { get; set; }

        public static Func<ImageGallery, ImageGalleryModel> Cast
        {
            get
            {
                return g => new ImageGalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImagesCount = g.Images.Count
                };
            }
        }
    }
}
