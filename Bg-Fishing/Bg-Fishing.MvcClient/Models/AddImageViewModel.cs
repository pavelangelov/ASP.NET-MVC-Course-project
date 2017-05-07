using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Models
{
    public class AddImageViewModel
    {
        [StringLength(
            Constants.InfoMaxLEngth,
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string ImageInfo { get; set; }
        
        public string SelectedImageGalleryId { get; set; }

        public IEnumerable<SelectListItem> GalleryNames { get; set; }
    }
}