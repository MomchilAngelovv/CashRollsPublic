namespace CashRolls.Web.Filters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    using CashRolls.Data.Models;
    using CashRolls.Services;

    public class RegisterErrorExceptionFilter : IAsyncExceptionFilter
    {
        private readonly UserManager<User> userManager;
        private readonly IErrorsService errorsService;

        public RegisterErrorExceptionFilter(
            UserManager<User> userManager,
            IErrorsService errorsService)
        {
            this.userManager = userManager;
            this.errorsService = errorsService;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var loggedUser = await userManager.GetUserAsync(context.HttpContext.User);


            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            var message = context.Exception.Message;
            var stackTrace = context.Exception.StackTrace;
            var userId = loggedUser?.Id;
            var information = loggedUser == null ? "Logged user is null." : null;

            await errorsService.RegisterAsync(method, path, message, stackTrace, userId, information);

            context.Result = new RedirectResult("/Home/Error");
        }
    }
}
