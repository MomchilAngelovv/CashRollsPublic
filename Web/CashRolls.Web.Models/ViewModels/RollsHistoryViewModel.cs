namespace CashRolls.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class RollsHistoryViewModel
    {
        public IEnumerable<RollsHistoryRoll> Rolls { get; set; }
    }
}
