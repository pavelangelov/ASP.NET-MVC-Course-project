using System.Collections.Generic;

using Bg_Fishing.DTOs.LakeDTOs;

namespace Bg_Fishing.DTOs.FishDTOs
{
    public class AllFishPropsDTO : FishDTO
    {
        public string Id { get; set; }

        public string Info { get; set; }

        public IEnumerable<LakeDTO> Lakes { get; set; }
    }
}
