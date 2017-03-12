using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;

namespace Bg_Fishing.Factories.Contracts
{
    public interface IFishFactory
    {
        Fish CreateFish();

        Fish CreateFish(string name, FishType fishType, string imageUrl);

        Fish CreateFish(string name, FishType fishType, string imageUrl, string info);
    }
}
