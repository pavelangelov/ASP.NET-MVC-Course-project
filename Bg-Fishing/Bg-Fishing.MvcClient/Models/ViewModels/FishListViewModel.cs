using System.Collections.Generic;

using Bg_Fishing.DTOs.FishDTOs;

namespace Bg_Fishing.MvcClient.Models.ViewModels
{
    public class FishListViewModel
    {
        public AllFishPropsDTO SelectedFish { get; set; }

        public IEnumerable<FishDTO> FishCollection { get; set; }
    }
}