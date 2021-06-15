using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string CreditCardNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
