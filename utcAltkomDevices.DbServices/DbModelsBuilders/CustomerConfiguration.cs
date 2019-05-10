using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.DbServices.DbModelsBuilders
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // If you want to make a diffrent prop as key
            //builder
            //    .HasKey(p => p.Id);

            // Some custom prop rules
            builder
                .Property(p => p.FirstName)
                .HasMaxLength(40);

            builder
                .Property(p => p.Name)
                .HasMaxLength(40)
                .IsRequired(true);
        }
    }
}
