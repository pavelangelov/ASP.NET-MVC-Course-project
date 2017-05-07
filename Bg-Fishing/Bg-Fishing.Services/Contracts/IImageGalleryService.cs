using System.Collections.Generic;

using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface IImageGalleryService
    {
        void Add(ImageGallery gallery);

        ImageGallery FindById(string id);

        IEnumerable<ImageGalleryModel> GetByLake(string lakeName);

        void AddImageToGallery(Image image, string galleryId);

        void AddImageToGallery(string galleryName, Image image);

        IEnumerable<ImageModel> GetAllImages(string galleryId);

        IEnumerable<ImageModel> GetAllUnconfirmed();

        int Save();
    }
}
