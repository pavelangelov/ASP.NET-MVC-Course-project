using System;
using System.ComponentModel.DataAnnotations;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Models.Contracts.Galleries;

namespace Bg_Fishing.Models.Galleries
{
    public class Image : IImage, IIdentifiable
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsConfirmed = false;
        }

        public Image(string imageUrl, DateTime date)
            : this()
        {
            this.ImageUrl = imageUrl;
            this.Date = date;
        }

        public Image(string imageUrl, DateTime date, string info)
            : this(imageUrl, date)
        {
            this.Info = info;
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

        public bool IsConfirmed { get; set; }
    }
}
