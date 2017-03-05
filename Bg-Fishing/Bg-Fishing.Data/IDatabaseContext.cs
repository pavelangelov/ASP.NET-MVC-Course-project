using System.Data.Entity;

using Bg_Fishing.Models;
using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Data
{
    public interface IDatabaseContext
    {
        IDbSet<Fish> Fish { get; }
        IDbSet<Lake> Lakes { get; }
        IDbSet<Location> Locations { get; }
        IDbSet<Image> Images { get; }
        IDbSet<Video> Videos { get; }
        IDbSet<ImageGallery> ImageGalleries { get; }
        IDbSet<VideoGallery> VideoGalleries { get; }

        int Save();
    }
}
