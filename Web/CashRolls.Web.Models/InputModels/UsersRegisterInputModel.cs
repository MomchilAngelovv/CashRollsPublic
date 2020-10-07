namespace CashRolls.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CashRolls.Web.Models.Common;

    public class UsersRegisterInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = ErrorMessages.PasswordsMustMatch)]
        public string ConfirmPassword { get; set; }
    }
}
