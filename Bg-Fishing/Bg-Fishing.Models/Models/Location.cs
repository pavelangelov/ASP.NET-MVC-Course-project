using System;

using Bg_Fishing.Models.Contracts;

namespace Bg_Fishing.Models
{
    public class Location : ILocation, IIdentifiable
    {
        public Location(double latitude, double longitude)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public string Id { get; set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public string Info { get; set; }
    }
}
