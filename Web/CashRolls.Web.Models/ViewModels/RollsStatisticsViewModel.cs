namespace CashRolls.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class RollsStatisticsViewModel
    {
        public IEnumerable<RollsStatisticsRoll> Rolls { get; set; }
    }
}
