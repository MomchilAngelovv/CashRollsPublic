namespace CashRolls.Data.Models
{
    using System;

    using CashRolls.Data.Models.Common;

    public class Error : IEntityMetaData
    {
        public Error()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastModiefiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
