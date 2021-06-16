using PaymentService.Domain.DataTransfer;
using PaymentService.Infrastructure.Subsidiaries.SubContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Subsidiaries.SubAgreements
{
    public class PremiumPaymentGateway : IPremiumPaymentGateway
    {
        public Task<bool> MakePremiumPayment(RecievePaymentDto model)
        {
            throw new NotImplementedException();
        }
    }
}
