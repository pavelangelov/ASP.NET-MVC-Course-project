using System;

using Bg_Fishing.Models.Contracts;

namespace Bg_Fishing.Models
{
    public class Location : ILocation, IIdentifiable
    {
        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Location(double latitude, double longitude)
            : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public Location(double latitude, double longitude, string info)
            : this(latitude, longitude)
        {
            this.Info = info;
        }

        public string Id { get; set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public string Info { get; set; }
    }
}
