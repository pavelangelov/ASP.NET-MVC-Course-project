using System.Collections.Generic;

using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Models
{
    public class HomeViewModel
    {
        public IEnumerable<NewsModel> News { get; set; }

        public bool HasMoreNews { get; set; }

        public int NextPage { get; set; }
    }
}