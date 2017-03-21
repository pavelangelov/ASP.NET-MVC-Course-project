using Bg_Fishing.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bg_Fishing.MvcClient.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [DataType(DataType.Password)]
        [Display(Name = ViewModelsDisplayNames.Password_DisplayName)]
        public string Password { get; set; }

        [Display(Name = "Запомни?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [EmailAddress]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [StringLength(
            Constants.PasswordMaxLength,
            MinimumLength = Constants.PasswordMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [DataType(DataType.Password)]
        [Display(Name = ViewModelsDisplayNames.Password_DisplayName)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ViewModelsDisplayNames.ConfirmPassword_DisplayName)]
        [Compare("Password", 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [StringLength(
            Constants.NameMaxLength, 
            MinimumLength = Constants.NameMinLength, 
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [Display(Name = ViewModelsDisplayNames.FirstName_DisplayName)]
        public string FirstName { get; set; }
        
        [StringLength(
            Constants.NameMaxLength, 
            MinimumLength = 0,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages), 
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [Display(Name = ViewModelsDisplayNames.MiddleName_DisplayName)]
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [StringLength(
            Constants.NameMaxLength,
            MinimumLength = Constants.NameMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [Display(Name = ViewModelsDisplayNames.LastName_DisplayName)]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [Range(
            Constants.AgeMinValue, 
            Constants.AgeMaxValue, 
            ErrorMessage = "Годините на потребителя трябва да са в интервал 6 - 125")]
        [Display(Name = "Години")]
        public int Age { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [EmailAddress]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [StringLength(
            Constants.PasswordMaxLength,
            MinimumLength = Constants.PasswordMinLength,
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [DataType(DataType.Password)]
        [Display(Name = ViewModelsDisplayNames.Password_DisplayName)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = ViewModelsDisplayNames.ConfirmPassword_DisplayName)]
        [Compare("Password",
            ErrorMessageResourceType = typeof(Resources.ValidationMessages),
            ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ValidationMessages),
                    ErrorMessageResourceName = "PropertyValueRequired")]
        [EmailAddress]
        [Display(Name = ViewModelsDisplayNames.Email_DisplayName)]
        public string Email { get; set; }
    }
}
