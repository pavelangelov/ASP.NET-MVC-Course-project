using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LakeViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [Display(Name = ViewModelsDisplayNames.LakeName_DisplayName)]
        
        public string Name { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [Display(Name = ViewModelsDisplayNames.LocationName_DisplayName)]
        public string LocationName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        public double Latitude { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        public double Longitude { get; set; }

        [Display(Name = ViewModelsDisplayNames.LakeInfo_DisplayName)]
        public string Info { get; set; }
    }
}