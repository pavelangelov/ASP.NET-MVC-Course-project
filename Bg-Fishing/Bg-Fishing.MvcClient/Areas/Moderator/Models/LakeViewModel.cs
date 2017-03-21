using Bg_Fishing.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LakeViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = ViewModelsDisplayNames.LakeName_DisplayName)]
        
        public string Name { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = ViewModelsDisplayNames.LocationName_DisplayName)]
        public string LocationName { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        public double Latitude { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "PropertyValueRequired")]
        public double Longitude { get; set; }

        [Display(Name = ViewModelsDisplayNames.LakeInfo_DisplayName)]
        public string Info { get; set; }
    }
}