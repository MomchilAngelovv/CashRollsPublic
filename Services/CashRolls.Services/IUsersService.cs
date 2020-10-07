namespace CashRolls.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CashRolls.Services.Models.ServiceModels;

    public interface IUsersService
    {
        bool HasUserWithPhoneNumber(string phoneNumber);
        IEnumerable<T> GetAll<T>();
        Task<UsersCreateContactMessageServiceModel> CreateContactMessage(string sender, string message);
        IEnumerable<T> GetLastContactMessages<T>();
    }
}
