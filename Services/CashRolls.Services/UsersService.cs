namespace CashRolls.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CashRolls.Data;
    using CashRolls.Services.Models.ServiceModels;
    using CashRolls.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly CashRollDbContext db;
        private readonly IMapper mapper;

        public UsersService(
            CashRollDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<UsersCreateContactMessageServiceModel> CreateContactMessage(string sender, string message)
        {
            var contactMessage = new ContactMessage
            {
                Sender = sender,
                Message = message
            };

            await this.db.ContactMessages.AddAsync(contactMessage);
            await this.db.SaveChangesAsync();

            var serviceModel = new UsersCreateContactMessageServiceModel
            {
                Id = contactMessage.Id
            };

            return serviceModel;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.db.Users
                .ProjectTo<T>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public IEnumerable<T> GetLastContactMessages<T>()
        {
            var lastContactMessages = this.db.ContactMessages
                .OrderByDescending(cm => cm.CreatedOn)
                .Take(10)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();

            return lastContactMessages;
        }

        public bool HasUserWithPhoneNumber(string phoneNumber)
        {
            var user = this.db.Users
                .SingleOrDefault(u => u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
