using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class AddNewsViewModel
    {
        [Required( 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Заглавие")]
        [StringLength(
            100, 
            MinimumLength = 3, 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string Title { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Съдържание")]
        [StringLength(
            3500,
            MinimumLength = 10,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string Content { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Линк към снимката.")]
        [StringLength(
            100,
            MinimumLength = 12,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string ImageUrl { get; set; }
    }
}