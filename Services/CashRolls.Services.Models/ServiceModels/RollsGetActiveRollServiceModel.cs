namespace CashRolls.Services.Models
{
    using System;

    public class RollsGetActiveRollServiceModel
    {
        public string Id { get; set; }
        public decimal EntryPrice { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string CurrencySymbol { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
