using PaymentService.Domain.DataTransfer;
using System;

namespace PaymentServiceAPITest
{
    public static class ErrorModelHelper
    {
        public static RecievePaymentDto GetPaymentErrorModelForAmount()
        {
            return new RecievePaymentDto
            {
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456"
            };
        }

        public static RecievePaymentDto GetPaymentErrorModelForSecurityCode()
        {
            return new RecievePaymentDto
            {
                Amount = 234m,
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456"
            };
        }
        public static RecievePaymentDto GetPremiumPaymentErrorModelForExpiryDate()
        {
            return new RecievePaymentDto
            {
                Amount= 455m,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now,
                CreditCardNumber = "1234567890123456"
            };
        }
        public static RecievePaymentDto GetPremiumPaymentErrorModelCreditCardNumber()
        {
            return new RecievePaymentDto
            {
                Amount = 345.45m,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "123456789eeeee456"
            };
        }
    }
}
