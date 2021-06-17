using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServiceAPITest.Helpers
{
    public class PremiumModelHelper
    {
        public static RecievePaymentDto PremiumPaymentDTO()
        {
            return new RecievePaymentDto
            {
                Amount = 4596.89M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456",
                CardHolder = "Chibuikem"
            };
        }

        public static Payment PremiumPaymentModel()
        {
            return new Payment
            {
                Amount = 4596.89M,
                SecurityCode = "123",
                ExpiryDate = DateTime.Now.AddDays(70),
                CreditCardNumber = "1234567890123456",
                CardHolder = "Chibuikem"
            };
        }

        public static GatewayResult PremiumGatewayResultTrue()
        {
            return new GatewayResult
            {
                Amount = 4596.89M,
                Status = true
            };
        }

        public static GatewayResult PremiumGatewayResultFalse()
        {
            return new GatewayResult
            {
                Amount = 4596.89M,
                Status = false
            };
        }

        public static PaymentResultDTO ReturnPremiumPaymentDTO()
        {
            return new PaymentResultDTO
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentStatus = "Processed"
            };
        }
    }
}
