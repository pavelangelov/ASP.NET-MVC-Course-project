using System.Collections.Generic;

using Bg_Fishing.Services.Models;

namespace Bg_Fishing.MvcClient.Models
{
    public class GetCommentsViewModel
    {
        public string LakeName { get; set; }

        public int PrevPage { get; set; }

        public int NextPage { get; set; }

        public bool HasPrev { get; set; }

        public bool HasNext { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }
    }
}