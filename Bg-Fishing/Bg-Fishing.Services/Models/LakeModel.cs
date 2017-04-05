using System;
using System.Collections.Generic;
using System.Linq;

using Bg_Fishing.Models;

namespace Bg_Fishing.Services.Models
{
    public class LakeModel
    {
        public string Id { get; private set; }
        
        public string Name { get; set; }
        
        public string Info { get; set; }
        
        public LocationModel Location { get; set; }
        
        public IEnumerable<FishModel> Fish { get; private set; }

        public IEnumerable<CommentModel> Comments { get; set; }

        public static Func<Lake, LakeModel> CastMinInfo
        {
            get
            {
                return l => new LakeModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Info = l.Info
                };
            }
        }

        public static Func<Lake, LakeModel> Cast
        {
            get
            {
                return l => new LakeModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Info = l.Info,
                    Location = LocationModel.Cast(l.Location),
                    Fish = l.Fish.Select(FishModel.CastWithIncludedLakes),
                    Comments = l.Comments.Select(CommentModel.Cast)
                };
            }
        }
    }
}