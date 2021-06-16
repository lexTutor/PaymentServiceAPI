using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Response<PaymentResultDTO>> MakePayment(RecievePaymentDto model);
    }
}
