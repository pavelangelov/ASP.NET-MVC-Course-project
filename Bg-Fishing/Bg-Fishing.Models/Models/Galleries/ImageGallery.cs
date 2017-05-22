using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Models.Contracts.Galleries;

namespace Bg_Fishing.Models.Galleries
{
    public class ImageGallery : IImageGallery, IIdentifiable
    {
        private string name;

        public ImageGallery()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
        }

        public ImageGallery(string name, string lakeId)
            : this()
        {
            this.Name = name;
            this.LakeId = lakeId;
        }

        public string Id { get; private set; }
        
        /// <summary>
        /// Get gallery name.
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(25)]
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
        public virtual ICollection<Image> Images { get; set; }
    }
}
