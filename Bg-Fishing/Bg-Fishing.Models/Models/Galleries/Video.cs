﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Bg_Fishing.Models.Galleries
{
    public class Video
    {
        public Video(string title, string url, DateTime postedon)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = title;
            this.Url = url;
            this.PostedOn = postedon;
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
