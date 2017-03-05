using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Enums;

namespace Bg_Fishing.Models
{
    public class Fish
    {
        private string name;
        private string info;

        public Fish(string name, FishType fishType)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Lakes = new HashSet<Lake>();
        }

        public string Id { get; private set; }

        /// <summary>
        /// Get fish name.
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
        /// Get or Set the additional info about the fish.
        /// </summary>
        public string Info
        {
            get
            {
                return this.info;
            }

            set
            {
                // TODO: Validate

                this.info = value;
            }
        }

        /// <summary>
        /// Get fish type.
        /// </summary>
        public FishType FishType { get; private set; }

        /// <summary>
        /// Get collection of lakes where this fish is available.
        /// </summary>
        public virtual ICollection<Lake> Lakes { get; set; }
    }
}
