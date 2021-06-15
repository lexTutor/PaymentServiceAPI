using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Persistence.EntityConfigurations
{
    public class EntityConfigurationsForPayment : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(payment => payment.PaymentStatus)
                .WithOne(PaymentStatus => PaymentStatus.Payment)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
