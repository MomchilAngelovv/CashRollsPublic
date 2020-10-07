namespace CashRolls.Services
{
    using System.Threading.Tasks;

    using CashRolls.Data;
    using CashRolls.Data.Models;
    using CashRolls.Services.Models;

    public class ErrorsService : IErrorsService
    {
        private readonly CashRollDbContext db;

        public ErrorsService(
            CashRollDbContext db)
        {
            this.db = db;
        }

        public async Task<ErrorsRegisterServiceModel> RegisterAsync(string method, string path, string message, string stackTrace, string userId, string information)
        {
            var error = new Error
            {
                Method = method,
                Path = path,
                Message = message,
                StackTrace = stackTrace,
                UserId = userId,
                Information = information
            };

            await db.Errors.AddAsync(error);
            await db.SaveChangesAsync();

            var serviceModel = new ErrorsRegisterServiceModel
            {

            };

            return serviceModel;
        }
    }
}
