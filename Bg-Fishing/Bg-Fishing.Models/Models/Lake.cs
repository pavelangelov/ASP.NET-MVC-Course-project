using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Models
{
    public class Lake : ILake, IIdentifiable
    {
        private string name;
        private string info;
        private Location location;

        public Lake()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Fish = new HashSet<Fish>();
        }

        public Lake(string name, Location location)
            : this()
        {
            this.Name = name;
            this.Location = location;
        }


        public Lake(string name, Location location, string info)
            : this(name, location)
        {
            this.Info = info;
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
        [MinLength(Constants.NameMinLength)]
        [MaxLength(Constants.NameMaxLength)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                var minLength = Constants.NameMinLength;
                var maxLength = Constants.NameMaxLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Name", minLength, maxLength);

                Utils.Validator.ValidateStringLength(value, maxLength, minLength, "Name", errorMessage);

                this.name = value;
            }
        }

        /// <summary>
        /// Get or Set additional info about the lake.
        /// </summary>
        [StringLength(Constants.InfoMaxLEngth)]
        public string Info
        {
            get
            {
                return this.info;
            }

            set
            {
                var minLength = 0;
                var maxLength = Constants.InfoMaxLEngth;
                var errorMessage = GlobalMessages.InfoErrorMessage;

                Utils.Validator.ValidateStringLength(value, maxLength, minLength, "Info", errorMessage);

                this.info = value;
            }
        }

        /// <summary>
        /// Get lake location.
        /// </summary>
        [Required]
        public Location Location
        {
            get
            {
                return this.location;
            }

            private set
            {
                Utils.Validator.ValidateForNull(value, "Location");

                this.location = value;
            }
        }

        /// <summary>
        /// Get collection of available fish in the lake.
        /// </summary>
        public virtual ICollection<Fish> Fish { get; private set; }
    }
}
