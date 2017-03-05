using System;
using System.ComponentModel.DataAnnotations;

namespace Bg_Fishing.Models.Galleries
{
    public class Image
    {
        public Image(string imageUrl)
        {
            this.Id = Guid.NewGuid().ToString();
            this.ImageUrl = imageUrl;
        }
        
        /// <summary>
        /// Get image Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Get image url.
        /// </summary>
        [Required]
        public string ImageUrl { get; private set; }

        /// <summary>
        /// Get or Set image date.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Get or set info about the image.
        /// </summary>
        public string Info { get; set; }
    }
}
