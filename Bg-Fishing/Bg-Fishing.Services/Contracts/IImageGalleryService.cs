using System.Collections.Generic;

using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface IImageGalleryService
    {
        void Add(ImageGallery gallery);

        /// <summary>
        /// Get Image Gallery entity that match the Id.
        /// </summary>
        /// <param name="id">The Id of the gallery entity.</param>
        /// <returns></returns>
        ImageGallery FindById(string id);

        /// <summary>
        /// Mark image as confirmed.
        /// </summary>
        /// <param name="imageId">The Id of the image.</param>
        void ConfirmImage(string imageId);

        /// <summary>
        /// Get all Galleries that contains images for passed lake name.
        /// </summary>
        /// <param name="lakeName">The name of the Lake.</param>
        /// <returns>Returns <see cref="IEnumerable{ImageGalleryModel}"/></returns>
        IEnumerable<ImageGalleryModel> GetByLake(string lakeName);

        /// <summary>
        /// Add passed <see cref="Image"/> to gallery.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="galleryId">The Id of the gallery.</param>
        void AddImageToGallery(Image image, string galleryId);

        /// <summary>
        /// Add passed <see cref="Image"/> to gallery.
        /// </summary>
        /// <param name="galleryName">The name of the gallery.</param>
        /// <param name="image">The image.</param>
        void AddImageToGallery(string galleryName, Image image);

        /// <summary>
        /// Get all images from the gallery.
        /// </summary>
        /// <param name="galleryId">Gallery Id.</param>
        /// <returns>Returns <see cref="IEnumerable{ImageModel}"/></returns>
        IEnumerable<ImageModel> GetAllImages(string galleryId);

        /// <summary>
        /// Get all galleries that have unconfirmed images.
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{ImageGalleryModel}"/></returns>
        IEnumerable<ImageGalleryModel> GetGalleriesWithUnconfirmedImages();

        /// <summary>
        /// Get all unconfirmed images.
        /// </summary>
        /// <param name="galleryId">The Id of the gallery.</param>
        /// <returns>Returns <see cref="IEnumerable{ImageModel}"/></returns>
        IEnumerable<ImageModel> GetAllUnconfirmed(string galleryId);

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns>Returns <see cref="System.Int32"/></returns>
        int Save();
    }
}
