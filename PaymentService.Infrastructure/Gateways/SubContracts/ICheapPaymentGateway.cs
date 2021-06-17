using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubContracts
{
    public interface ICheapPaymentGateway
    {
        GatewayResult MakeCheapPayment(RecievePaymentDto model);
    }
}
