namespace CashRolls.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using CashRolls.Data.Models.Common;

    public class Role : IdentityRole<string>, IEntityMetaData
    {
        public Role()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
