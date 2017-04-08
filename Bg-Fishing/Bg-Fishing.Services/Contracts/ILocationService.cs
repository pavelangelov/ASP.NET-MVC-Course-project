using System.Collections.Generic;

using Bg_Fishing.Models;
using Bg_Fishing.Services.Models;

namespace Bg_Fishing.Services.Contracts
{
    public interface ILocationService
    {
        IEnumerable<LocationModel> GetAll();

        Location FindByName(string name);

        void Add(Location location);

        int Save();
    }
}
