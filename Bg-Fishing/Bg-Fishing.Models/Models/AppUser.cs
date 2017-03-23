using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNet.Identity.EntityFramework;

using Bg_Fishing.Models.Contracts;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Models
{
    public class AppUser : IdentityUser, IAppUser
    {
        private string firstName;
        private string middleName;
        private string lastName;
        private int age;

        public AppUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public AppUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        /// <summary>
        /// Get or Set user type.
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Get or Set user age.
        /// </summary>
        [Range(Constants.AgeMinValue, Constants.AgeMaxValue)]
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                var maxValue = Constants.AgeMaxValue;
                var minValue = Constants.AgeMinValue;
                var errorMessage = string.Format(GlobalMessages.AgeErrorMessage, minValue, maxValue);
                Utils.Validator.ValidateInteger(value, maxValue, minValue, paramName: "Age", errorMessage: errorMessage);

                this.age = value;
            }
        }

        /// <summary>
        /// Get or Set first name.
        /// </summary>
        [Required]
        [StringLength(Constants.NameMaxLength, MinimumLength = Constants.NameMinLength)]   
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                var maxLength = Constants.NameMaxLength;
                var minLength = Constants.NameMinLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Името", minLength, maxLength);
                Utils.Validator.ValidateStringLength(value, maxLength, minLength, paramName: "FirstName", errorMessage: errorMessage);

                this.firstName = value;
            }
        }

        /// <summary>
        /// Get or Set middle name.
        /// </summary>
        [StringLength(Constants.NameMaxLength, MinimumLength = 0)]
        public string MiddleName
        {
            get
            {
                return this.middleName;
            }

            set
            {
                if (value == null)
                {
                    this.middleName = value;
                    return;
                }

                var maxLength = Constants.NameMaxLength;
                var minLength = 0;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Презимето", minLength, maxLength);
                Utils.Validator.ValidateStringLength(value, maxLength, minLength, paramName: "MiddleName", errorMessage: errorMessage);

                this.middleName = value;
            }
        }

        /// <summary>
        /// Get or Set last name.
        /// </summary>
        [Required]
        [StringLength(Constants.NameMaxLength, MinimumLength = Constants.NameMinLength)]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                var maxLength = Constants.NameMaxLength;
                var minLength = Constants.NameMinLength;
                var errorMessage = string.Format(GlobalMessages.NameErrorMessage, "Фамилията", minLength, maxLength);
                Utils.Validator.ValidateStringLength(value, maxLength, minLength, paramName: "LastName", errorMessage: errorMessage);

                this.lastName = value;
            }
        }

        /// <summary>
        /// Get or Set avatar.
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
