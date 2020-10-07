namespace CashRolls.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using CashRolls.Data.Models.Common;

    public class User : IdentityUser<string>, IEntityMetaData
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public virtual ICollection<RollUser> RollsUsers { get; set; }
        public virtual ICollection<Roll> WonRolls { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
