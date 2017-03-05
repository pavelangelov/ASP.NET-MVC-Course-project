using System.Collections.Generic;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Models.Contracts.Galleries
{
    public interface IVideoGallery
    {
        /// <summary>
        /// Get or Set gallery name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get all videos from the gallery.
        /// </summary>
        ICollection<Video> Videos{ get; }
    }
}
