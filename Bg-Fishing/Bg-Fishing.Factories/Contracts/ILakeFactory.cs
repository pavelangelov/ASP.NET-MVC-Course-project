using Bg_Fishing.Models;
using Bg_Fishing.Models.Contracts;

namespace Bg_Fishing.Factories.Contracts
{
    public interface ILakeFactory
    {
        Lake CreateLake();

        Lake CreateLake(string name, ILocation location);

        Lake CreateLake(string name, ILocation location, string info);
    }
}
