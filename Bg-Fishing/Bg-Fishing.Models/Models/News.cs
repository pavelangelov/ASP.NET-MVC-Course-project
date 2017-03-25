using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Models
{
    public class News : INews, IIdentifiable
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;
        public const int ContentMinLength = 10;
        public const int ContentMaxLength = 3500;

        private string title;
        private string content;
        private string imageUrl;

        public News()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<NewsComment>();
        }

        public News(string title, string content, string imageUrl, DateTime postedOn)
            : this()
        {
            this.Title = title;
            this.Content = content;
            this.ImageUrl = imageUrl;
            this.PostedOn = postedOn;
        }

        /// <summary>
        /// Gets Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                Utils.Validator.ValidateStringLength(value, TitleMaxLength, TitleMinLength, paramName: "Title");

                this.title = value;
            }
        }

        /// <summary>
        /// Gets or Sets Content.
        /// </summary>
        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                Utils.Validator.ValidateStringLength(value, ContentMaxLength, ContentMinLength, paramName: "Content");

                this.content = value;
            }
        }

        /// <summary>
        /// Gets or Sets Image url.
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }

            set
            {
                if (value == null)
                {
                    this.imageUrl = Constants.NewsDefaultImage;
                }
                else
                {
                    this.imageUrl = value;
                }
            }
        }

        /// <summary>
        /// Gets Posted date.
        /// </summary>
        public DateTime PostedOn { get; private set; }

        /// <summary>
        /// Gets Comments.
        /// </summary>
        public virtual ICollection<NewsComment> Comments { get; private set; }
    }
}
