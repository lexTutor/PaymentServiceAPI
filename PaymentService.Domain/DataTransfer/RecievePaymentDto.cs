using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.DataTransfer
{
    public class RecievePaymentDto
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
