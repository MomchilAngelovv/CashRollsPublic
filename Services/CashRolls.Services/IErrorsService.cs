namespace CashRolls.Services
{
    using System.Threading.Tasks;

    using CashRolls.Services.Models;

    public interface IErrorsService
    {
        Task<ErrorsRegisterServiceModel> RegisterAsync(string method, string path, string message, string stackTrace, string userId, string information);
    }
}
