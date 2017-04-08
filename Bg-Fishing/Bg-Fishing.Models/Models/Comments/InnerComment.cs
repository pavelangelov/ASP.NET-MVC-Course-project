using System;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Models.Contracts;

namespace Bg_Fishing.Models.Comments
{
    public class InnerComment : IInnerComment, IIdentifiable
    {
        public InnerComment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public InnerComment(string content, string username, DateTime postedDate)
            : this()
        {
            this.Content = content;
            this.Username = username;
            this.PostedDate = postedDate;
        }

        public string Id { get; private set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PostedDate { get; private set; }

        [Required]
        public string Username { get; private set; }
    }
}
