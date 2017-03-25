using System.Collections.Generic;

using Bg_Fishing.DTOs;

namespace Bg_Fishing.MvcClient.Models
{
    public class HomeViewModel
    {
        public IEnumerable<NewsDTO> News { get; set; }

        public bool HasMoreNews { get; set; }

        public int NextPage { get; set; }
    }
}