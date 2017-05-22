using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;
using System.Data.Entity;

namespace Bg_Fishing.Services
{
    public class ImageGalleryService : IImageGalleryService
    {
        private IDatabaseContext dbContext;

        public ImageGalleryService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, paramName: "dbContext");

            this.dbContext = dbContext;
        }

        public void Add(ImageGallery gallery)
        {
            this.dbContext.ImageGalleries.Add(gallery);
        }

        public void AddImageToGallery(Image image, string galleryId)
        {
            var gallery = this.dbContext.ImageGalleries.Find(galleryId);

            gallery.Images.Add(image);
        }

        public void AddImageToGallery(string galleryName, Image image)
        {
            var gallery = this.dbContext.ImageGalleries.FirstOrDefault(g => g.Name == galleryName);

            gallery.Images.Add(image);
        }

        public ImageGallery FindById(string id)
        {
            var gallery = this.dbContext.ImageGalleries.Find(id);

            return gallery;
        }

        public void ConfirmImage(string imageId)
        {
            this.dbContext.Images.Find(imageId).IsConfirmed = true;
        }

        public IEnumerable<ImageModel> GetAllImages(string galleryId)
        {
            return this.dbContext.ImageGalleries.Find(galleryId)
                                                        .Images
                                                        .Where(i => i.IsConfirmed)
                                                        .Select(ImageModel.Cast);
        }

        public IEnumerable<ImageGalleryModel> GetByLake(string lakeName)
        {
            var gallery = this.dbContext.ImageGalleries.Include(g => g.Images)
                                                        .Where(g => g.Lake.Name == lakeName)
                                                        .Select(ImageGalleryModel.Cast);

            return gallery;
        }

        public IEnumerable<ImageModel> GetAllUnconfirmed(string galleryId)
        {
            return this.dbContext.ImageGalleries.Include(g => g.Images)
                                                .FirstOrDefault(g => g.Id == galleryId)
                                                .Images
                                                .Where(i => !i.IsConfirmed)
                                                .Select(ImageModel.Cast);
        }
        
        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
