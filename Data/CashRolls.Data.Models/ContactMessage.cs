using CashRolls.Data.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashRolls.Data.Models
{
    public class ContactMessage : IEntityMetaData
    {
        public ContactMessage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
