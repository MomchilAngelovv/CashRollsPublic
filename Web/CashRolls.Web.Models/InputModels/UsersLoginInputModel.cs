namespace CashRolls.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class UsersLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
