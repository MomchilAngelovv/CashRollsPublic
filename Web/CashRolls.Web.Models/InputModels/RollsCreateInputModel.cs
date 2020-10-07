namespace CashRolls.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CashRolls.Web.Models.Common;

    public class RollsCreateInputModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.SelectValidCurrency)]
        public int CurrencyId { get; set; }
        [Range(1, int.MaxValue)]
        public decimal EntryPrice { get; set; }
        [Range(1, 100)]
        public decimal EntryPriceTaxPercent { get; set; }
        [Range(1, int.MaxValue)]
        public decimal MaxSum { get; set; }
    }
}
