using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg_Fishing.Models.Galleries
{
    public class VideoGallery
    {
        public VideoGallery(string name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Videos = new HashSet<Video>();
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
        public string Name { get; set; }

        /// <summary>
        /// Get all videos from the gallery.
        /// </summary>
        public virtual ICollection<Video> Videos { get; private set; }
    }
}
