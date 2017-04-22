using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Models.Enums;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class AddFishViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [StringLength(
            Constants.NameMaxLength,
            MinimumLength = Constants.NameMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string FishName { get; set; }

        public FishType FishType { get; set; }
        
        [StringLength(
            Constants.InfoMaxLEngth,
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string Info { get; set; }
    }
}