using System.Collections.Generic;

using Bg_Fishing.DTOs.FishDTOs;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface IFishService
    {
        void Add(Fish fish);

        Fish FindByName(string name);

        AllFishPropsDTO GetFishDTOByName(string name);

        IEnumerable<FishDTO> GetAll();

        IEnumerable<FishDTO> GetAllByType(FishType fishType);

        int Save();
    }
}
