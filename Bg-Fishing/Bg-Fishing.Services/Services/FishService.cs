using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.DTOs.FishDTOs;
using Bg_Fishing.DTOs.LakeDTOs;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Services
{
    public class FishService : IFishService
    {
        IDatabaseContext dbContext;

        public FishService(IDatabaseContext dbContext)
        {
            Validator.ValidateForNull(dbContext, "dbContext");

            this.dbContext = dbContext;
        }

        public Fish FindByName(string name)
        {
            var fish = this.dbContext.Fish.FirstOrDefault(f => f.Name == name);

            return fish;
        }

        public AllFishPropsDTO GetFishDTOByName(string name)
        {
            var fish = this.dbContext.Fish.Include(f => f.Lakes).FirstOrDefault(f => f.Name == name);

            if (fish != null)
            {
                return new AllFishPropsDTO
                {
                    Id = fish.Id,
                    Name = fish.Name,
                    ImageUrl = fish.ImageUrl,
                    Info = fish.Info,
                    Lakes = fish.Lakes.Select(l => new LakeDTO
                    {
                        Name = l.Name,
                        Id = l.Id
                    })
                };
            }

            return null;
        }

        public IEnumerable<FishDTO> GetAll()
        {
            var allFish = this.dbContext.Fish;

            if (allFish != null)
            {
                return allFish.Select(f => new FishDTO
                {
                    Name = f.Name,
                    ImageUrl = f.ImageUrl
                });
            }

            return null;
        }

        public IEnumerable<FishDTO> GetAllByType(FishType fishType)
        {
            var allFish = this.dbContext.Fish.Where(f => f.FishType == fishType);

            if (allFish != null)
            {
                return allFish.Select(f => new FishDTO
                {
                    Name = f.Name,
                    ImageUrl = f.ImageUrl
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
