using System.Collections.Generic;

using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface IVideoService
    {
        IEnumerable<VideoGalleryModel> GetAll();

        VideoModel GetVideoById(string id);

        string GetGalleryNameById(string galleryId);

        IEnumerable<VideoModel> GetVideosFromGallery(string galleryId);

        void AddVideoToGallery(string galleryName, Video video);

        bool RemoveVideoFromGallery(string galleryName, string videoId);

        int Save();
    }
}
