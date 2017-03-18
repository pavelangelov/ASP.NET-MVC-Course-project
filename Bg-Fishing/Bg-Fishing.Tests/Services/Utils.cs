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

        public static IList<Location> GetLocationsCollection()
        {
            var collection = new List<Location>
            {
                new Location(1.1, 1.1, "First"),
                new Location(2.1, 2.1, "Second"),
                new Location(3.1, 3.1 ,"Third")
            };

            return collection;
        }
    }
}
