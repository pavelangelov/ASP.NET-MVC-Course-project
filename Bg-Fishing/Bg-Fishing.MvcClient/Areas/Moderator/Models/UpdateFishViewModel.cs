using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Utils;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Areas.Moderator.Models
{
    public class UpdateFishViewModel
    {
        [Required(
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = Constants.RequiredPropertyResourceKey)]
        public string SelectedLake { get; set; }

        [Display(Name = ViewModelsDisplayNames.SelectedFish_DisplayName)]
        public IEnumerable<string> SelectedFish { get; set; }
        
        [Display(Name = ViewModelsDisplayNames.Lakes_DisplayName)]
        public IEnumerable<LakeModel> Lakes { get; set; }
        
        public IEnumerable<FishModel> Fish { get; set; }
    }
}