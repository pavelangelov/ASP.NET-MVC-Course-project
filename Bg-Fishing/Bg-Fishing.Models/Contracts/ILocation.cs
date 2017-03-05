namespace Bg_Fishing.Models.Contracts
{
    public interface ILocation
    {
        double Latitude { get; }

        double Longitude { get; }

        string Info { get; }
    }
}
