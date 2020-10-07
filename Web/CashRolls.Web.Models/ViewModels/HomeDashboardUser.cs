namespace CashRolls.Web.Models.ViewModels
{
    public class HomeDashboardUser
    {
        public bool AlreadyJoined { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdministrator { get; set; }
        public string Email { get; set; }
    }
}
