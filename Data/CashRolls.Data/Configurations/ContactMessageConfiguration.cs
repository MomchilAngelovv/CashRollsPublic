using CashRolls.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashRolls.Data.Configurations
{
    public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
    {
        public void Configure(EntityTypeBuilder<ContactMessage> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Sender).HasMaxLength(250).IsRequired();
            entity.Property(entity => entity.Message).HasMaxLength(450).IsRequired();
        }
    }
}
