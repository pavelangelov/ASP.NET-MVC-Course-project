using System;
using System.Collections.Generic;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;
using System.ComponentModel.DataAnnotations;

namespace Bg_Fishing.Models.Comments
{
    public class Comment : IComment, IIdentifiable
    {
        private string lakeName;
        private string username;
        private string content;

        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<InnerComment>();
        }

        public Comment(string lakeName, string username, string content, DateTime postedDate)
            : this()
        {
            this.LakeName = lakeName;
            this.Username = username;
            this.Content = content;
            this.PostedDate = postedDate;
        }

        public string Id { get; private set; }

        [Required]
        [MinLength(1)]
        [MaxLength(Constants.CommentContentMaxLength)]
        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                var maxLength = Constants.CommentContentMaxLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Content", 1, maxLength);

                Utils.Validator.ValidateStringLength(value, maxLength, paramName: "Content", errorMessage: errorMessage);

                this.content = value;
            }
        }

        [Required]
        public string LakeName
        {
            get
            {
                return this.lakeName;
            }

            set
            {
                var minLength = Constants.NameMinLength;
                var maxLength = Constants.NameMaxLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "LakeName", minLength, maxLength);

                Utils.Validator.ValidateStringLength(value, maxLength, minLength, "LakeName", errorMessage);

                this.lakeName = value;
            }
        }

        public DateTime PostedDate { get; private set; }

        [Required]
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {

                Utils.Validator.ValidateForNull(value, paramName: "Username");

                this.username = value;
            }
        }

        public virtual ICollection<InnerComment> Comments { get; set; }
    }
}
