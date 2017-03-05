using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg_Fishing.Models.Galleries
{
    public class ImageGallery
    {
        private string name;

        public ImageGallery(string name, string lakeId)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.LakeId = lakeId;
            this.Images = new HashSet<Image>();
        }

        public string Id { get; private set; }
        
        /// <summary>
        /// Get gallery name.
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                // TODO: Validate

                this.name = value;
            }
        }

        /// <summary>
        /// Get or Set lake Id.
        /// </summary>
        [Required]
        public string LakeId { get; set; }

        public virtual Lake Lake { get; set; }

        /// <summary>
        /// Get all images from the gallery.
        /// </summary>
        public virtual ICollection<Image> Images { get; private set; }
    }
}
