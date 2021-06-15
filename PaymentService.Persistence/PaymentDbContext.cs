using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;
using PaymentService.Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentService.Persistence
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options): base(options)
        {

        }
      public  DbSet<Payment> Payments { get; set; }
      public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Applies the entity configuration for the Payment model
            builder.ApplyConfiguration(new EntityConfigurationsForPayment());

            //Applies the entity configurations for the Payment Status model.
            builder.ApplyConfiguration(new EntityConfigurationsForPaymentStatus());

            //This is to convert any decimal in the models to doubles as sqlite does not support decimal values.
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(prop => prop.PropertyType
                    == typeof(decimal));

                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion<double>();
                    }
                }
            }
        }
    }
}
