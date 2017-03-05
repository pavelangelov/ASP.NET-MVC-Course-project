using System.Collections.Generic;

using Bg_Fishing.Models.Enums;

namespace Bg_Fishing.Models.Contracts
{
    public interface IFish
    {
        /// <summary>
        /// Get fish name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get or Set the additional info about the fish.
        /// </summary>
        string Info { get; set; }

        /// <summary>
        /// Get fish type.
        /// </summary>
        FishType FishType { get; }

        /// <summary>
        /// Get collection of lakes where this fish is available.
        /// </summary>
        ICollection<Lake> Lakes { get; }
    }
}
