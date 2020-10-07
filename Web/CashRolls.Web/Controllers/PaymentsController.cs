namespace CashRolls.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Stripe;
    using Stripe.Checkout;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using CashRolls.Services;
    using CashRolls.Web.Common;
    using CashRolls.Data.Models;

    public class PaymentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRollsService rollsService;

        public PaymentsController(
            UserManager<User> userManager,
            IRollsService rollsService)
        {
            this.userManager = userManager;
            this.rollsService = rollsService;
        }

        [Authorize]
        public async Task<IActionResult> StripePaymentSessionId()
        {
            var loggedUser = await userManager.GetUserAsync(User);
            var activeRoll = rollsService.GetActiveRoll();
            if (activeRoll == null)
            {
                return BadRequest(ErrorMessages.NoActiveRoll);
            }

            var priceStripeFormat = Convert.ToInt64(activeRoll.EntryPrice) * 100;
            var currencyStripeFormat = activeRoll.CurrencyIsoCode;

            var itemsToPurchase = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currencyStripeFormat,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Description = $"CashRoll Id: {activeRoll.Id}",
                            Name = $"Entry fee - CashRolls {activeRoll.CreatedOn.ToShortDateString()} - {activeRoll.EntryPrice:0.00}{activeRoll.CurrencySymbol}",
                        },
                        UnitAmount = priceStripeFormat,
                    },
                    Quantity = 1,
                }
            };

            var rollUser = await rollsService.PendingJoinAsync(activeRoll.Id, loggedUser.Id);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                CustomerEmail = loggedUser.Email,
                LineItems = itemsToPurchase,
                Mode = "payment",
                SuccessUrl = $"Your success payment url",
                CancelUrl = "Your cancel payment url",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Json(session.Id);
        }
    }
}
