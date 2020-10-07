namespace CashRolls.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using CashRolls.Services;
    using CashRolls.Web.Common;
    using CashRolls.Data.Models;
    using CashRolls.Web.Models.ViewModels;
    using CashRolls.Web.Models.InputModels;
    using System.Security.Claims;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        private readonly IRollsService rollsService;
        private readonly IUsersService usersService;

        public HomeController(
            UserManager<User> userManager,
            IRollsService rollsService,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.rollsService = rollsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new HomeDashboardViewModel();

            var loggedUser = await userManager.GetUserAsync(User);
            var activeRoll = rollsService.GetActiveRoll<HomeDashboardRoll>();

            if (activeRoll != null)
            {
                viewModel.ActiveRoll = activeRoll;
            }

            if (loggedUser != null)
            {
                var isUserJoined = this.rollsService.IsUserJoined(loggedUser.Id, activeRoll?.Id);

                var user = new HomeDashboardUser
                {   
                    AlreadyJoined = isUserJoined,
                    IsLoggedIn = loggedUser != null,
                    IsAdministrator = await userManager.IsInRoleAsync(loggedUser, Roles.Administrator),
                    Email = loggedUser.Email
                };

                viewModel.User = user;
            }

            return this.View(viewModel);
        }
        [HttpGet]
        public IActionResult Rules()
        {
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var inputModel = new HomeContactsInputModel
            {
                Sender = loggedUser?.Email
            };

            return this.View(inputModel);
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacts(HomeContactsInputModel inputModel)
        {
            await this.usersService.CreateContactMessage(inputModel.Sender, inputModel.Message);
            this.TempData[InfoMessages.MessageSentSuccessfully] = InfoMessages.MessageSentSuccessfully;
            return this.RedirectToAction(nameof(Dashboard));
        }
    }
}
