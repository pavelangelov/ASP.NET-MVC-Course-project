using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.MvcClient.Models
{
    public class NewsDetailsViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        [Display(Name = ViewModelsDisplayNames.ContentProperty_DisplayName)]
        [StringLength(Constants.CommentContentMaxLength, 
            MinimumLength = 1, 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = Constants.StringLengthResourceKey)]
        public string Content { get; set; }

        public string NewsId { get; set; }

        public NewsModel News { get; set; }
    }
}