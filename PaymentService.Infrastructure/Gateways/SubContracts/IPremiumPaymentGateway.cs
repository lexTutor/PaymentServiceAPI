using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubContracts
{
    /// <summary>
    /// Payment Contract for premium payments above $500
    /// </summary>
    public interface IPremiumPaymentGateway
    {
        GatewayResult MakePremiumPayment(RecievePaymentDto model);
    }
}
