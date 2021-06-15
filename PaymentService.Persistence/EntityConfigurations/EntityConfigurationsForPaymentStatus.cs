using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Persistence.EntityConfigurations
{
    public class EntityConfigurationsForPaymentStatus : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.HasOne(PaymentStatus => PaymentStatus.Payment)
                 .WithOne(Payment => Payment.PaymentStatus)
                 .IsRequired();
        }
    }
}
