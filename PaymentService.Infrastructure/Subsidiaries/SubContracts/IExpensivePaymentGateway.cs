using PaymentService.Domain.DataTransfer;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Subsidiaries.SubContracts
{
    public interface IExpensivePaymentGateway
    {
        Task<bool> MakeExpensivePayment(RecievePaymentDto model);

    }
}
