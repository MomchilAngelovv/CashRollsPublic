namespace CashRolls.Data.Models
{
    using System;

    using CashRolls.Data.Models.Common;

    public class RollUser : IEntityMetaData
    {
        public RollUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string RollId { get; set; }
        public string UserId { get; set; }
        public bool IsPending { get; set; }

        public virtual Roll Roll { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
