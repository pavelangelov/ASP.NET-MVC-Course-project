using System.Collections.Generic;

using Bg_Fishing.DTOs.FishDTOs;
using Bg_Fishing.Models.Enums;

namespace Bg_Fishing.Services.Contracts
{
    public interface IFishService
    {
        AllFishPropsDTO FindByName(string name);

        IEnumerable<FishDTO> GetAll();

        IEnumerable<FishDTO> GetAllByType(FishType fishType);

        int Save();
    }
}
