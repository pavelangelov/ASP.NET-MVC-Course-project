﻿using System.Web.Http;

using Newtonsoft.Json;

using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.ApiControllers
{
    public class ImagesController : ApiController
    {
        private IImageGalleryService imageGalleryService;

        public ImagesController(IImageGalleryService imageGalleryService)
        {
            Validator.ValidateForNull(imageGalleryService, paramName: "imageGalleryService");

            this.imageGalleryService = imageGalleryService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public string GetUnconfirmedFromGallery(string id)
        {
            var images = this.imageGalleryService.GetAllUnconfirmed(id);

            var result = JsonConvert.SerializeObject(new { result = images });
            return result;
        }

        [HttpGet]
        public string GetAllGalleriesForLake(string name)
        {
            var galleries = this.imageGalleryService.GetByLake(name);

            return JsonConvert.SerializeObject(new { result = galleries });
        }

        [HttpGet]
        public string GetImagesFromGallery(string id)
        {
            var images = this.imageGalleryService.GetAllImages(id);

            return JsonConvert.SerializeObject(new { result = images });
        }
    }
}
