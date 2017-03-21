using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Models
{
    public class Location : ILocation, IIdentifiable
    {
        private string name;
        private string info;

        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Location(double latitude, double longitude, string name)
            : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = name;
        }

        public Location(double latitude, double longitude, string name, string info)
            : this(latitude, longitude, name)
        {
            this.Info = info;
        }

        public string Id { get; set; }

        [Index(IsUnique = true)]
        [MinLength(Constants.NameMinLength)]
        [MaxLength(Constants.LocationNameMaxLength)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                var minLength = Constants.NameMinLength;
                var maxLength = Constants.LocationNameMaxLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Name", minLength, maxLength);

                Utils.Validator.ValidateStringLength(value, maxLength, minLength, "Name", errorMessage);

                this.name = value;
            }
        }

        [Required]
        public double Latitude { get; private set; }

        [Required]
        public double Longitude { get; private set; }

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
    }
}
