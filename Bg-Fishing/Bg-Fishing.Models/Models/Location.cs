using System;

using Bg_Fishing.Models.Contracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bg_Fishing.Models
{
    public class Location : ILocation, IIdentifiable
    {
        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Location(double latitude, double longitude, string name)
            : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = name;
        }

        public Location(double latitude, double longitude, string name, string info)
            : this(latitude, longitude, name)
        {
            this.Info = info;
        }

        public string Id { get; set; }

        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; private set; }

        [Required]
        public double Longitude { get; private set; }

        public string Info { get; set; }
    }
}
