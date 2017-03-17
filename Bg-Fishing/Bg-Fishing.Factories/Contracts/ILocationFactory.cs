using Bg_Fishing.Models;

namespace Bg_Fishing.Factories.Contracts
{
    public interface ILocationFactory
    {
        Location CreateLocation();

        Location CreateLocation(double latitude, double longitude, string name);

        Location CreateLocation(double latitude, double longitude, string name, string info);
    }
}
