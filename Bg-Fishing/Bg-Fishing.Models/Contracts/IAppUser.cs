namespace Bg_Fishing.Models.Contracts
{
    public interface IAppUser
    {
        /// <summary>
        /// Get or Set user type.
        /// </summary>
        UserType UserType { get; set; }

        /// <summary>
        /// Get or Set user age.
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// Get or Set first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Get or Set middle name.
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// Get or Set last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Get or Set avatar.
        /// </summary>
        string AvatarUrl { get; set; }
    }
}
