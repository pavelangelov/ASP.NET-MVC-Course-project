using Bg_Fishing.Services.Models;
using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Models
{
    public class NewsDetailsViewModel
    {
        [Required]
        [Display(Name = "Коментар")]
        [StringLength(Constants.CommentContentMaxLength, 
            MinimumLength = 1, 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string Content { get; set; }

        public string NewsId { get; set; }

        public NewsModel News { get; set; }
    }
}