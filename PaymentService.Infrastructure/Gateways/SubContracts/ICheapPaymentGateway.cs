using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubContracts
{
    /// <summary>
    /// Payment Contract foe cheap payments less than $21
    /// </summary>
    public interface ICheapPaymentGateway
    {
        GatewayResult MakeCheapPayment(RecievePaymentDto model);
    }
}
