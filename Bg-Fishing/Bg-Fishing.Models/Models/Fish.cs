using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Enums;
using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Models
{
    public class Fish : IFish, IIdentifiable
    {
        private string name;
        private string info;

        public Fish()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lakes = new HashSet<Lake>();
        }

        public Fish(string name, FishType fishType, string imageUrl)
            : this()
        {
            this.Name = name;
            this.FishType = fishType;
            this.ImageUrl = imageUrl;
        }

        public Fish(string name, FishType fishType, string imageUrl, string info)
            : this(name, fishType, imageUrl)
        {
            this.Info = info;
        }

        public string Id { get; private set; }

        /// <summary>
        /// Get fish name.
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [StringLength(
            Constants.NameMaxLength, 
            MinimumLength = Constants.NameMinLength,
            ErrorMessage = GlobalMessages.FishNameErrorMessage)]
        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                // TODO: Validate

                this.name = value;
            }
        }

        /// <summary>
        /// Get or Set the additional info about the fish.
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
        /// Get fish type.
        /// </summary>
        public FishType FishType { get; private set; }

        /// <summary>
        /// Get or Set image url.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Get collection of lakes where this fish is available.
        /// </summary>
        public virtual ICollection<Lake> Lakes { get; private set; }
    }
}
