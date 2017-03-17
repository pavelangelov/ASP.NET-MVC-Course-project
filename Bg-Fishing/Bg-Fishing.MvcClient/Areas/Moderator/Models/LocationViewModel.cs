using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LocationViewModel
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Място")]
        public string Name { get; set; }

        [Display(Name = "Информация")]
        public string Info { get; set; }
    }
}