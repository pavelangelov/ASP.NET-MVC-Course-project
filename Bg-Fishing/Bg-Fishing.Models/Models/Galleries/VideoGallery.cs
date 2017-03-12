using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Models.Contracts.Galleries;

namespace Bg_Fishing.Models.Galleries
{
    public class VideoGallery : IVideoGallery, IIdentifiable
    {
        public VideoGallery()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Videos = new HashSet<Video>();
        }

        public VideoGallery(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>
        /// Get gallery Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Get or Set gallery name.
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(25)]
        public string Name { get; set; }

        /// <summary>
        /// Get all videos from the gallery.
        /// </summary>
        public virtual ICollection<Video> Videos { get; private set; }
    }
}
