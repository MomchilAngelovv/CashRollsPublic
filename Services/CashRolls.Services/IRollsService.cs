namespace CashRolls.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using CashRolls.Data.Models;
    using CashRolls.Services.Models;
    using CashRolls.Services.Models.ServiceModels;

    public interface IRollsService
    {
        Task<RollsCreateServiceModel> CreateAsync(decimal entryPrice, int currencyId, decimal entryPriceTaxPercent, decimal maxSum);

        RollsGetActiveRollServiceModel GetActiveRoll();
        T GetActiveRoll<T>();
        T GetById<T>(string id);
        IEnumerable<T> GetAll<T>();
        IEnumerable<T> GetParticipantsByPage<T>(string rollId, int page);
        IEnumerable<T> GetClosedRolls<T>();


        Task<RollsCloseServiceModel> CloseAsync();  
        Task<RollsRollWinnerServiceModel> RollWinnerAsync(string rollId);
        Task<RollsPendingJoinServiceModel> PendingJoinAsync(string rollId, string userId);
        Task<RollUser> ConfirmJoinAsync(string pendingJoinId, string userId);

        bool IsUserJoined(string userId, string rollId);
        bool HasActiveRoll();
    }
}
