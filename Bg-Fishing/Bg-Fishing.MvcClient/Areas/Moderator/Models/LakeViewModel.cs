using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LakeViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Име на Язовира")]
        
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public string LocationName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public double Latitude { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public double Longitude { get; set; }

        public string Info { get; set; }
    }
}