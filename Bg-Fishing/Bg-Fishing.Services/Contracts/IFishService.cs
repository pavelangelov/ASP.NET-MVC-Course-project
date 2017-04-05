using System.Collections.Generic;

using Bg_Fishing.Models.Enums;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface IFishService
    {
        void Add(Fish fish);

        Fish FindByName(string name);

        FishModel GetFishByName(string name);

        IEnumerable<FishModel> GetAll();

        IEnumerable<FishModel> GetAllByType(FishType fishType);

        int Save();
    }
}
