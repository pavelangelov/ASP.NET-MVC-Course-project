using System.Collections.Generic;

using Bg_Fishing.DTOs.LocationDTOs;
using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ILocationService
    {
        IEnumerable<LocationDTO> GetAll();

        Location FindByName(string name);

        void Add(Location location);

        int Save();
    }
}
