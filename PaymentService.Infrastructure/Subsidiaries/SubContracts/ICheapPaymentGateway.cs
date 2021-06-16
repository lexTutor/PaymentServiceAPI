using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Subsidiaries.SubContracts
{
    public interface ICheapPaymentGateway
    {
        Task<bool> MakeCheapPayment(RecievePaymentDto model);
    }
}
