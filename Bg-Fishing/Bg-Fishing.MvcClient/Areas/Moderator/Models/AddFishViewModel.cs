using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.DTOs.FishDTOs;
using Bg_Fishing.DTOs.LakeDTOs;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class AddFishViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages), ErrorMessageResourceName = "PropertyValueRequired")]
        public string SelectedLake { get; set; }

        [Display(Name = "Изберете риба/и")]
        public IEnumerable<string> SelectedFish { get; set; }
        
        [Display(Name = "Иберете язовир")]
        public IEnumerable<LakeDTO> Lakes { get; set; }
        
        public IEnumerable<FishDTO> Fish { get; set; }
    }
}