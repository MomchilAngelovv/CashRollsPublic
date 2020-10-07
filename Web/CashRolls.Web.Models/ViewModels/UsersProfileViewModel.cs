using System.Collections.Generic;

namespace CashRolls.Web.Models.ViewModels
{
    public class UsersProfileViewModel
    {
        public bool HasActiveRoll { get; set; }
        public UsersProfileUser User { get; set; }
        public IEnumerable<UsersProfileContactMessage> ContactMessages { get; set; }
    }
}
