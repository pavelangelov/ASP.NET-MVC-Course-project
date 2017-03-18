using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class LakeViewModel
    {
        [Display(Name = "Име на Язовира")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string Info { get; set; }
    }
}