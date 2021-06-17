using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using System;

namespace PaymentServiceAPITest.Helpers
{
    public class ExpensiveModelHelper
    {
        public static RecievePaymentDto ExpensivePaymentDTO()
        {
            return new RecievePaymentDto
            {
                Amount = 327.32M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456",
                CardHolder = "Chibuikem"
            };
        }

        public static Payment ExpensivePaymentModel()
        {
            return new Payment
            {
                Amount = 459.89M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CardHolder = "Chibuikem",
                CreditCardNumber = "1234567890123456"
            };
        }

        public static GatewayResult ExpensiveGatewayResultTrue()
        {
            return new GatewayResult
            {
                Amount = 459.89M,
                Status = true
            };
        }
        public static GatewayResult ExpensiveGatewayResultFalse()
        {
            return new GatewayResult
            {
                Amount = 459.89M,
                Status = false
            };
        }
        public static PaymentResultDTO ReturnExpensivePaymentDTO()
        {
            return new PaymentResultDTO
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentStatus = "Processed"
            };
        }
    }
}
