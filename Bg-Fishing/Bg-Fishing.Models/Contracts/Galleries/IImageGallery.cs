using System.Collections.Generic;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Models.Contracts.Galleries
{
    public interface IImageGallery
    {
        /// <summary>
        /// Get gallery name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get or Set lake Id.
        /// </summary>
        string LakeId { get; set; }

        /// <summary>
        /// Get all images from the gallery.
        /// </summary>
        ICollection<Image> Images { get; }
    }
}
