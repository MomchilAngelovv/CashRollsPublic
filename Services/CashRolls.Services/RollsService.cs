namespace CashRolls.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CashRolls.Data;
    using CashRolls.Data.Models;
    using CashRolls.Services.Models;
    using CashRolls.Services.Common;
    using CashRolls.Services.Models.ServiceModels;

    public class RollsService : IRollsService
    {
        private readonly CashRollDbContext db;
        private readonly ICurrenciesService currenciesService;
        private readonly IMapper mapper;

        public RollsService(
            CashRollDbContext db,
            ICurrenciesService currenciesService,
            IMapper mapper)
        {
            this.db = db;
            this.currenciesService = currenciesService;
            this.mapper = mapper;
        }

        public async Task<RollsCreateServiceModel> CreateAsync(decimal entryPrice, int currencyId, decimal entryPriceTaxPercent, decimal maxSum)
        {
            var activeRollFound = this.HasActiveRoll();
            if (activeRollFound == true)
            {
                throw new InvalidOperationException(ErrorMessages.AlreadyActiveRoll);
            }

            var currencyFound = this.currenciesService.IsPresent(currencyId);
            if (currencyFound == false)
            {
                throw new ArgumentException(ErrorMessages.CurrencyNotFound);
            }

            var roll = new Roll
            {
                IsActive = true,
                EntryFee = entryPrice,
                CurrencyId = currencyId,
                CutPercent = entryPriceTaxPercent,
                MaxSum = maxSum
            };

            await db.Rolls.AddAsync(roll);
            await db.SaveChangesAsync();

            var serviceModel = new RollsCreateServiceModel
            {

            };

            return serviceModel;
        }

        public RollsGetActiveRollServiceModel GetActiveRoll()
        {
            var serviceModel = db.Rolls
                .Where(r => r.IsActive == true)
                .Select(r => new RollsGetActiveRollServiceModel
                {
                    Id = r.Id,
                    EntryPrice = r.EntryFee,
                    CreatedOn = r.CreatedOn,
                    CurrencyIsoCode = r.Currency.IsoCode,
                    CurrencySymbol = r.Currency.Symbol
                })
                .SingleOrDefault();

            return serviceModel;
        }
        public T GetActiveRoll<T>()
        {
            var activeRoll = this.db.Rolls
                .Where(r => r.IsActive == true)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .SingleOrDefault();

            return activeRoll;
        }
        public T GetById<T>(string id)
        {
            return db.Rolls
                .Where(roll => roll.Id == id)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .SingleOrDefault();
        }
        public IEnumerable<T> GetAll<T>()
        {
            var allRolls = this.db.Rolls
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return allRolls;
        }
        public IEnumerable<T> GetParticipantsByPage<T>(string rollId, int page)
        {
            var pagedParticipants = this.db.RollsUsers
                .Where(ru => ru.RollId == rollId && ru.IsPending == false)
                .Select(ru => ru.User)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return pagedParticipants;
        }
        public IEnumerable<T> GetClosedRolls<T>()
        {
            return db.Rolls
                .Where(roll => roll.IsActive == false)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<RollsCloseServiceModel> CloseAsync()
        {
            var roll = this.db.Rolls
                .Include(r => r.RollsUsers)
                .SingleOrDefault(r => r.IsActive == true);

            if (roll == null)
            {
                throw new ArgumentException(ErrorMessages.RollNotFound);
            }

            roll.IsActive = false;
            roll.LastModiefiedOn = DateTime.UtcNow;
            roll.ParticipantsCount = roll.RollsUsers.Where(ru => ru.IsPending == false).Count();

            await db.SaveChangesAsync();

            var serviceModel = new RollsCloseServiceModel
            {

            };

            return serviceModel;
        }
        public async Task<RollsRollWinnerServiceModel> RollWinnerAsync(string rollId)
        {
            var roll = this.db.Rolls
                .Include(r => r.RollsUsers)
                .SingleOrDefault(r => r.Id == rollId);

            if (roll == null)
            {
                throw new ArgumentException(ErrorMessages.RollNotFound);
            }

            var participantIds = roll.RollsUsers
                .Select(ru => ru.UserId)
                .ToList();

            if (participantIds.Count > 0)
            {
                var randomIndex = new Random().Next(0, participantIds.Count);
                var luckyUserId = participantIds[randomIndex];

                roll.WinnerId = luckyUserId;
                roll.RolledOn = DateTime.UtcNow;

                await db.SaveChangesAsync();
            }

            var serviceModel = new RollsRollWinnerServiceModel
            {

            };

            return serviceModel;
        }
        public async Task<RollsPendingJoinServiceModel> PendingJoinAsync(string rollId, string userId)
        {
            var rollUser = new RollUser
            {
                RollId = rollId,
                UserId = userId,
                IsPending = true
            };

            await db.RollsUsers.AddAsync(rollUser);
            await db.SaveChangesAsync();

            var serviceModel = new RollsPendingJoinServiceModel
            {
                Id = rollUser.Id
            };

            return serviceModel;
        }
        public async Task<RollUser> ConfirmJoinAsync(string pendingJoinId, string userId)
        {
            var activeRoll = this.GetActiveRoll();
            if (activeRoll == null)
            {
                throw new InvalidOperationException(ErrorMessages.ActiveRollNotFound);
            }

            var pendingJoin = this.db.RollsUsers
                .SingleOrDefault(ru => ru.Id == pendingJoinId);

            if (pendingJoin == null)
            {
                throw new ArgumentException(ErrorMessages.PaymentError);
            }

            if (pendingJoin.UserId == userId && pendingJoin.RollId == activeRoll.Id && pendingJoin.IsPending == false)
            {
                throw new InvalidOperationException(ErrorMessages.UserAlreadyJoinedRoll);
            }

            pendingJoin.IsPending = false;
            pendingJoin.LastModiefiedOn = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return pendingJoin;
        }

        public bool IsUserJoined(string userId, string rollId)
        {
            if (rollId == null)
            {
                return false;
            };

            var isUserJoined = this.db.RollsUsers
                .Where(ru => ru.RollId == rollId && ru.IsPending == false)
                .Select(ru => ru.UserId)
                .Contains(userId);

            return isUserJoined;
        }
        public bool HasActiveRoll()
        {
            var roll = this.db.Rolls
                .SingleOrDefault(r => r.IsActive == true);

            if (roll == null)
            {
                return false;
            }

            return true;
        }
    }
}
