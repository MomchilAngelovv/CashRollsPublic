namespace CashRolls.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CashRolls.Data.Models.Common;

    public class Roll : IEntityMetaData
    {
        public Roll()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public bool IsActive { get; set; }
        public decimal EntryFee { get; set; }
        public decimal CutPercent { get; set; }
        public decimal? MaxSum { get; set; }
        public int ParticipantsCount { get; set; }
        public DateTime? RolledOn { get; set; }

        public string WinnerId { get; set; }
        public int CurrencyId { get; set; }

        public virtual User Winner { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<RollUser> RollsUsers { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
