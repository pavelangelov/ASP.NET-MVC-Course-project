using System;

namespace Bg_Fishing.Models.Contracts.Galleries
{
    public interface IVideo
    {
        /// <summary>
        /// Get or Set video title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Get video url.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Get video posted date.
        /// </summary>
        DateTime PostedOn { get; }
    }
}
