using System;
using System.Collections.Generic;

using Bg_Fishing.Models;
using Bg_Fishing.Models.Comments;
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

        public static IList<Lake> GetLakesCollection()
        {
            var matchedLocation = new Location() { Name = "Valid" };
            var collection = new List<Lake>
            {
                new Lake("First", matchedLocation),
                new Lake("Second", new Location() { Name = "Invalid"}),
                new Lake("Third", matchedLocation)
            };

            return collection;
        }

        public static IList<Comment> GetCommentsCollection()
        {
            var collection = new List<Comment>
            {
                new Comment() { LakeName =  "First lake", Username = "First user" },
                new Comment() { LakeName =  "Second lake", Username = "Second user" },
                new Comment() { LakeName =  "Third lake", Username = "Third user" }
            };

            return collection;
        }

        public static IList<Video> GetVideoCollection()
        {
            var collection = new List<Video>
            {
                new Video("first", "firstUrl", DateTime.Now),
                new Video("second", "secondUrl", DateTime.Now),
                new Video("third", "thirdUrl", DateTime.Now)
            };

            return collection;
        }

        public static IList<News> GetNewsCollection()
        {
            var collection = new List<News>
            {
                new News() { Title = "First" },
                new News() { Title = "Second" },
                new News() { Title = "Third" }
            };

            return collection;
        }
    }
}
