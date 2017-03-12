using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IImageGalleryFactory
    {
        ImageGallery CreateImageGallery();

        ImageGallery CreateImageGallery(string name, string lakeId);
    }
}
