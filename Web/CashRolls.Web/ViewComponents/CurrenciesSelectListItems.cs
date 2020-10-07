namespace CashRolls.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using CashRolls.Services;

    public class CurrenciesSelectListItems : ViewComponent
    {
        private readonly ICurrenciesService currenciesService;

        public CurrenciesSelectListItems(
            ICurrenciesService currenciesService)
        {
            this.currenciesService = currenciesService;
        }

        public IViewComponentResult Invoke()
        {
            var crrenciesSelectListItems = currenciesService.GetAll<SelectListItem>();
            return this.View(crrenciesSelectListItems);
        }
    }
}
