using System;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Models.Contracts.Galleries;

namespace Bg_Fishing.Models.Galleries
{
    public class Video : IVideo, IIdentifiable
    {
        public Video()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Video(string title, string url, DateTime postedOn)
            : this()
        {
            this.Title = title;
            this.Url = url;
            this.PostedOn = postedOn;
        }

        public string Id { get; private set; }

        /// <summary>
        /// Get or Set video title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get video url.
        /// </summary>
        [Required]
        public string Url { get; private set; }

        /// <summary>
        /// Get video posted date.
        /// </summary>
        public DateTime PostedOn { get; private set; }
    }
}
