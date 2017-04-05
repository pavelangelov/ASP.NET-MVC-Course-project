using System;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class LocationModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public double Latitude { get;  set; }
        
        public double Longitude { get;  set; }
        
        public string Info { get; set; }

        public static Func<Location, LocationModel> Cast
        {
            get
            {
                return l => new LocationModel()
                {
                    Id = l.Id,
                    Name = l.Name,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Info = l.Info
                };
            }
        }
    }
}