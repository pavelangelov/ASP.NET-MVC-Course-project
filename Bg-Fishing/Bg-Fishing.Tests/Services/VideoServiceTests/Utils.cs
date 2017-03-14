using Bg_Fishing.Models.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Tests.Services.VideoServiceTests
{
    public class Utils
    {
        public static IList<VideoGallery> GetVideoGalleriesCollection()
        {
            var collection = new List<VideoGallery>
            {
                new VideoGallery("First"),
                new VideoGallery("Second"),
                new VideoGallery("Third")
            };

            return collection;
        }

        public static IList<VideoGallery> GetEmptyVideoGallery()
        {
            return new List<VideoGallery>();
        }
    }
}
