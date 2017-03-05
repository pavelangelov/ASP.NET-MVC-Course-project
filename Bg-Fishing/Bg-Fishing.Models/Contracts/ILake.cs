using System.Collections.Generic;

namespace Bg_Fishing.Models.Contracts
{
    public interface ILake
    {
        /// <summary>
        /// Get or Set lake name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get lake location.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Get collection of available fish in the lake.
        /// </summary>
        ICollection<Fish> Fish { get; }
    }
}
