namespace CashRolls.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomeDashboardRoll
    {
        public string Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string EntryPrice { get; set; }
        public string EntryPriceStripe { get; set; }
        public string ParticipantsCount { get; set; }
        public string Reward { get; set; }
        public string CreatedOn { get; set; }
        public string EntryPriceTaxPercent { get; set; }
        public IEnumerable<string> ParticipantIds { get; set; }
    }
}
