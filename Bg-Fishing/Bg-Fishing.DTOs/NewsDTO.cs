using System;
using System.Collections.Generic;

namespace Bg_Fishing.DTOs
{
    public class NewsDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<NewsCommentDTO> Comments { get; set; }
    }
}
