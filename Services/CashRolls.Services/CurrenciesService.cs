namespace CashRolls.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CashRolls.Data;

    public class CurrenciesService : ICurrenciesService
    {
        private readonly CashRollDbContext db;
        private readonly IMapper mapper;

        public CurrenciesService(
            CashRollDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.db.Currencies
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .ToList();
        }
        public T GetById<T>(int id)
        {
            return this.db.Currencies
                .Where(currency => currency.Id == id)
                .ProjectTo<T>(mapper.ConfigurationProvider)
                .SingleOrDefault();
        }
        public bool IsPresent(int id)
        {
            var currency = this.db.Currencies
                .SingleOrDefault(c => c.Id == id);

            if (currency == null)
            {
                return false;   
            }

            return true;
        }
    }
}
