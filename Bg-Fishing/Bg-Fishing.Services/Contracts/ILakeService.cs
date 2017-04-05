using System.Collections.Generic;

using Bg_Fishing.Models;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ILakeService
    {
        IEnumerable<LakeModel> GetAll();

        void Add(Lake lake);

        Lake FindByName(string name);

        IEnumerable<LakeModel> FindByLocation(string locationName);

        int Save();
    }
}
