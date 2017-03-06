using Bg_Fishing.Models.Galleries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Services.Contracts
{
    public interface IVideoService
    {
        void AddVideoToGallery(string galleryName, Video video);

        bool RemoveVideoFromGallery(string galleryName, string videoId);

        int Save();
    }
}
