using System.Linq;
using System.Collections.Generic;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs.LocationDTOs;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class LocationService : ILocationService
    {
        private IDatabaseContext dbContext;

        public LocationService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, "dbContext");

            this.dbContext = dbContext;
        }

        public void Add(Location location)
        {
            this.dbContext.Locations.Add(location);
        }

        public AllLocationPropsDTO FindByName(string name)
        {
            var location = this.dbContext.Locations.FirstOrDefault(l => l.Name == name);

            if (location != null)
            {
                return new AllLocationPropsDTO
                {
                    Name = location.Name,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Info = location.Info
                };
            }

            return null;
        }

        public IEnumerable<LocationDTO> GetAll()
        {
            var allLocations = this.dbContext.Locations;

            if (allLocations != null)
            {
                return allLocations.Select(l => new LocationDTO
                {
                    Name = l.Name
                });
            }

            return null;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
