using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Contracts;

namespace Bg_Fishing.Models
{
    public class Lake : ILake, IIdentifiable
    {
        private string name;
        private ILocation location;

        public Lake(string name, ILocation location)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Location = location;
            this.Fish = new HashSet<Fish>();
        }

        /// <summary>
        /// Get lake Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Get or Set lake name.
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
        /// Get or Set additional info about the lake.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Get lake location.
        /// </summary>
        [Required]
        public ILocation Location
        {
            get
            {
                return this.location;
            }

            private set
            {
                // TODO: Validate
                this.location = value;
            }
        }

        /// <summary>
        /// Get collection of available fish in the lake.
        /// </summary>
        public virtual ICollection<Fish> Fish { get; private set; }
    }
}
