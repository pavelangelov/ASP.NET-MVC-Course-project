using System.Collections.Generic;

using Bg_Fishing.DTOs.LakeDTOs;
using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ILakeService
    {
        IEnumerable<LakeDTO> GetAll();

        void Add(Lake lake);

        Lake FindByName(string name);

        IEnumerable<LakeDTO> FindByLocation(string locationName);

        int Save();
    }
}
