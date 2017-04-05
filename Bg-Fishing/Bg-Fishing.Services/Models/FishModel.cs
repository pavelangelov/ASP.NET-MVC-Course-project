using System;
using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class FishModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Info { get; set; }
        
        public string FishType { get; set; }
        
        public string ImageUrl { get; set; }
        
        public virtual IEnumerable<LakeModel> Lakes { get; set; }

        public static Func<Fish, FishModel> CastWithoutIncludeLakes
        {
            get
            {
                return f => new FishModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    FishType = f.FishType.ToString(),
                    ImageUrl = f.ImageUrl,
                    Info = f.Info
                };
            }
        }

        public static Func<Fish, FishModel> CastWithIncludedLakes
        {
            get
            {
                return f => new FishModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    FishType = f.FishType.ToString(),
                    ImageUrl = f.ImageUrl,
                    Info = f.Info,
                    Lakes = f.Lakes.Select(LakeModel.CastMinInfo)
                };
            }
        }
    }
}
