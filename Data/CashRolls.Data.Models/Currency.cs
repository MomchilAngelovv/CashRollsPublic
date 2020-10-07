namespace CashRolls.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CashRolls.Data.Models.Common;

    public class Currency : IEntityMetaData
    {
        public Currency()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Roll> Rolls { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
