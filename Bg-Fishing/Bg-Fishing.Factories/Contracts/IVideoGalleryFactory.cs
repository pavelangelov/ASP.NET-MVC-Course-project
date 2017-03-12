using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IVideoGalleryFactory
    {
        VideoGallery CreateVideoGallery();

        VideoGallery CreateVideoGallery(string name);
    }
}
