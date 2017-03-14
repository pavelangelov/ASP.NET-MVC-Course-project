using System.Collections.Generic;

using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Tests.Services
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

        public static IList<Fish> GetFishCollection()
        {
            var collection = new List<Fish>
            {
                new Fish("First", FishType.FreshAndSaltWather, "some url"),
                new Fish("Second", FishType.FreshAndSaltWather, "some url"),
                new Fish("Third", FishType.FreshAndSaltWather, "some url")
            };

            return collection;
        }
    }
}
