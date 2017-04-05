using System.Collections.Generic;

using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Models.ViewModels
{
    public class FishListViewModel
    {
        public FishModel SelectedFish { get; set; }

        public IEnumerable<FishModel> FishCollection { get; set; }
    }
}