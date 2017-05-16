using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Bg_Fishing.Utils;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Models
{
    public class AddImageViewModel
    {
        public AddImageViewModel()
        {
            this.AvailableGalleries = new List<ImageGalleryModel>();
        }

        [StringLength(
            Constants.InfoMaxLEngth,
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        [Display(Name = "Информация")]
        public string ImageInfo { get; set; }

        [Display(Name = "Избери категория")]
        public string SelectedLakeId { get; set; }

        [Display(Name = "Избери категория")]
        public string SelectedImageGalleryId { get; set; }

        [Required(ErrorMessage = "Не е избрана галерия към която да се добави снимката!")]
        [StringLength(
            Constants.NameMaxLength,
            MinimumLength = Constants.NameMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        [Display(Name = "Избери съществуваща галерия")]
        public string Name { get; set; }

        public IEnumerable<LakeModel> Lakes { get; set; }

        public IEnumerable<ImageGalleryModel> AvailableGalleries { get; set; }
    }
}