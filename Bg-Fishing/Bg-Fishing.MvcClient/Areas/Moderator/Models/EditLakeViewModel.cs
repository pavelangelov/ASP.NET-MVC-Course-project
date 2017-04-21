using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class EditLakeViewModel
    {
        public string OldName { get; set; }

        [Display(Name = "Промяна на името")]
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        [StringLength(
            Constants.NameMaxLength,
            MinimumLength = Constants.NameMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string LakeName { get; set; }

        [StringLength(
            Constants.InfoMaxLEngth,
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string LakeInfo { get; set; }
    }
}