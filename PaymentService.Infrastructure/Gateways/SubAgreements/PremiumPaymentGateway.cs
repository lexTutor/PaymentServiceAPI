using PaymentService.Domain.DataTransfer;
using PaymentService.Infrastructure.Gateways.SubContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubAgreements
{
    public class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        /// <summary>
        /// Makes a cheap payment and returns the status of the payment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GatewayResult MakePremiumPayment(RecievePaymentDto model)
        {
            //Returns a payment status based on a random decision to simulate the possibility of a failed or successful payment.
            Random random = new Random();
            int value = random.Next(12, 67);
            return new GatewayResult { Status = value % 2 == 0, Amount = model.Amount };
        }
    }
}
