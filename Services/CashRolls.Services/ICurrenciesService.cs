namespace CashRolls.Services
{
    using System.Collections.Generic;

    public interface ICurrenciesService
    {
        IEnumerable<T> GetAll<T>();
        T GetById<T>(int id);
        bool IsPresent(int id);
    }
}
