using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class AddNewsViewModel
    {
        [Required( 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [Display(Name = ViewModelsDisplayNames.NewsTitle_DisplayName)]
        [StringLength(
            100, 
            MinimumLength = 3, 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string Title { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [Display(Name = ViewModelsDisplayNames.NewsContent_DisplayName)]
        [StringLength(
            3500,
            MinimumLength = 10,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string Content { get; set; }
    }
}