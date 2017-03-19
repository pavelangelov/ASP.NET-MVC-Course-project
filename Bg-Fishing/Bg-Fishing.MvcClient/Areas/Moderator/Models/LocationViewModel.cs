using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LocationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public double Latitude { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public double Longitude { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Местоположение")]
        public string LocationName { get; set; }

        [Display(Name = "Информация")]
        public string Info { get; set; }
    }
}