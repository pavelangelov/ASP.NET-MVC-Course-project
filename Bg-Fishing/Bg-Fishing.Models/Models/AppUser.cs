using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNet.Identity.EntityFramework;

namespace Bg_Fishing.Models
{
    public class AppUser : IdentityUser
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
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                // TODO: Validate
                this.age = value;
            }
        }

        /// <summary>
        /// Get or Set first name.
        /// </summary>
        [Required]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                // TODO: Validate

                this.firstName = value;
            }
        }

        /// <summary>
        /// Get or Set middle name.
        /// </summary>
        public string MiddleName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                // TODO: Validate for max length

                this.firstName = value;
            }
        }

        /// <summary>
        /// Get or Set last name.
        /// </summary>
        [Required]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                // TODO: Validate

                this.lastName = value;
            }
        }

        /// <summary>
        /// Get or Set avatar.
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
