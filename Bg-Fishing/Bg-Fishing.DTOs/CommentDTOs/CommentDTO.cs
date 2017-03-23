using System;
using System.Collections.Generic;

namespace Bg_Fishing.DTOs.CommentDTOs
{
    public class CommentDTO
    {
        public string LakeName { get; set; }

        public string Username { get; set; }

        public string Content { get; set; }

        public DateTime PostedDate { get; set; }

        public IEnumerable<InnerCommentDTO> Comments { get; set; }
    }
}
