using System.Collections.Generic;

using Bg_Fishing.DTOs.LakeDTOs;

namespace Bg_Fishing.DTOs.FishDTOs
{
    public class FishDTO
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<LakeDTO> Lakes { get; set; }
    }
}
