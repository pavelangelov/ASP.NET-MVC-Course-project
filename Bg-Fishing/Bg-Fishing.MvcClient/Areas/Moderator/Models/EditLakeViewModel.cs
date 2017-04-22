using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class EditLakeViewModel
    {
        public string OldName { get; set; }

        [Display(Name = ViewModelsDisplayNames.NewLakeName_DisplayName)]
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [StringLength(
            Constants.NameMaxLength,
            MinimumLength = Constants.NameMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string LakeName { get; set; }

        [StringLength(
            Constants.InfoMaxLEngth,
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string LakeInfo { get; set; }
    }
}