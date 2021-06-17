using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using System;

namespace PaymentServiceAPITest
{
    public static class CheapModelHelper
    {
        public static RecievePaymentDto CheapPaymentDTO()
        {
            return new RecievePaymentDto
            {
                Amount = 19.09M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456",
                CardHolder = "Chibuikem"
            };
        }
        public static Payment CheapPaymentModel()
        {
            return new Payment
            {
                Amount = 16.19M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456",
                CardHolder = "Chibuikem"
            };
        }

        public static GatewayResult CheapGatewayReturnTrue()
        {
            return new GatewayResult
            {
                Amount = 16.19M,
                Status = true
            };
        }
        public static GatewayResult CheapGatewayReturnFalse()
        {
            return new GatewayResult
            {
                Amount = 16.19M,
                Status = false
            };
        }

        public static PaymentResultDTO ReturnCheapPaymentDTO()
        {
            return new PaymentResultDTO
            {
                PaymentId= Guid.NewGuid().ToString(),
                PaymentStatus = "Processed"
            };
        }
    }
}
