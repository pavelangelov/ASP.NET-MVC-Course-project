using System;

namespace Bg_Fishing.Models.Contracts.Galleries
{
    public interface IImage
    {
        /// <summary>
        /// Get image url.
        /// </summary>
        string ImageUrl { get; }

        /// <summary>
        /// Get or Set image date.
        /// </summary>
        DateTime? Date { get; set; }

        /// <summary>
        /// Get or set info about the image.
        /// </summary>
        string Info { get; set; }
    }
}
