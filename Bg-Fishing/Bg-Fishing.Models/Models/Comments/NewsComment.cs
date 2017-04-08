using System;

namespace Bg_Fishing.Models.Comments
{
    public class NewsComment
    {
        public NewsComment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public NewsComment(string content, string username, DateTime postedOn)
            : this()
        {
            this.Content = content;
            this.Username = username;
            this.PostedOn = postedOn;
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
