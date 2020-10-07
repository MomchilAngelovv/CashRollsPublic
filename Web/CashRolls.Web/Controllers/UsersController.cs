namespace CashRolls.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using CashRolls.Services;
    using CashRolls.Web.Common;
    using CashRolls.Data.Models;
    using CashRolls.Web.Models.ViewModels;
    using CashRolls.Web.Models.InputModels;

    public class UsersController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        private readonly IRollsService rollsService;
        private readonly IUsersService usersService;

        public UsersController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRollsService rollsService,
            IUsersService usersService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.rollsService = rollsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var loggedUser = await userManager.GetUserAsync(User);
            var activeRoll = rollsService.GetActiveRoll();

            var user = new UsersProfileUser
            {
                Id = loggedUser.Id,
                PhoneNumber = loggedUser.PhoneNumber,
                Email = loggedUser.Email,
                IsAdministrator = await userManager.IsInRoleAsync(loggedUser, Roles.Administrator)
            };

            var lastContactMessages = this.usersService.GetLastContactMessages<UsersProfileContactMessage>();

            var viewModel = new UsersProfileViewModel
            {
                HasActiveRoll = activeRoll != null,
                User = user,
                ContactMessages = lastContactMessages
            };

            return this.View(viewModel);
        }
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public IActionResult All()
        {
            var users = usersService.GetAll<UsersAllUser>();
            var viewModel = new UsersAllViewModel
            {
                Users = users
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsersLoginInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var result = await signInManager.PasswordSignInAsync(inputModel.Email, inputModel.Password, inputModel.RememberMe, true);

            if (result.Succeeded == false)
            {
                TempData[ErrorMessages.InvalidLoginCredentials] = ErrorMessages.InvalidLoginCredentials;
                return this.View();
            }

            return this.Redirect("/Home/Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> Register(UsersRegisterInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            var userByEmail = await userManager.FindByEmailAsync(inputModel.Email);
            var phoneAlreadyTaken = this.usersService.HasUserWithPhoneNumber(inputModel.PhoneNumber);

            if (userByEmail != null || phoneAlreadyTaken)
            {
                TempData[ErrorMessages.UserAlreadyExists] = ErrorMessages.UserAlreadyExists;
                return this.View(inputModel);
            }

            var newUser = new User
            {
                UserName = inputModel.Email,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
            };

            var result = await userManager.CreateAsync(newUser, inputModel.Password);

            if (result.Succeeded == false)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Login));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
