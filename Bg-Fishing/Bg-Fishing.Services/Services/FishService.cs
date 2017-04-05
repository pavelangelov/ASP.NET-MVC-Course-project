using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Services.Models;
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

        public void Add(Fish fish)
        {
            this.dbContext.Fish.Add(fish);
        }

        public Fish FindByName(string name)
        {
            var fish = this.dbContext.Fish.FirstOrDefault(f => f.Name == name);

            return fish;
        }

        public FishModel GetFishByName(string name)
        {
            var fish = this.dbContext.Fish.Include(f => f.Lakes).FirstOrDefault(f => f.Name == name);

            if (fish != null)
            {
                return FishModel.CastWithIncludedLakes(fish);
            }

            return null;
        }

        public IEnumerable<FishModel> GetAll()
        {
            var allFish = this.dbContext.Fish;

            if (allFish != null)
            {
                return allFish.Select(FishModel.CastWithoutIncludeLakes);
            }

            return null;
        }

        public IEnumerable<FishModel> GetAllByType(FishType fishType)
        {
            var allFish = this.dbContext.Fish.Where(f => f.FishType == fishType);

            if (allFish != null)
            {
                return allFish.Select(FishModel.CastWithoutIncludeLakes);
            }

            return null;
        }

        public int Save()
        {
            return this.dbContext.Save();
        }
    }
}
