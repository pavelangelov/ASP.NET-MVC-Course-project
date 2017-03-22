using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs.LakeDTOs;
using Bg_Fishing.Models;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;
using System.Data.Entity;

namespace Bg_Fishing.Services
{
    public class LakeService : ILakeService
    {
        private IDatabaseContext dbContext;

        public LakeService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, paramName: "dbContext");

            this.dbContext = dbContext;
        }

        public void Add(Lake lake)
        {
            this.dbContext.Lakes.Add(lake);
        }

        public IEnumerable<LakeDTO> FindByLocation(string locationName)
        {
            var lakes = this.dbContext.Lakes.Where(l => l.Location.Name.Contains(locationName))
                                            .Select(l => new LakeDTO
                                            {
                                                Name = l.Name,
                                                Id = l.Id
                                            });

            return lakes;
        }

        public Lake FindByName(string name)
        {
            var lake = this.dbContext.Lakes.Include(l => l.Location).FirstOrDefault(l => l.Name == name);

            return lake;
        }

        public IEnumerable<LakeDTO> GetAll()
        {
            var lakes = this.dbContext.Lakes.Select(l => new LakeDTO
            {
                Name = l.Name,
                Id = l.Id
            });

            return lakes;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
