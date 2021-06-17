using PaymentService.Domain.DataTransfer;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubContracts
{
    public interface IExpensivePaymentGateway
    {
        GatewayResult MakeExpensivePayment(RecievePaymentDto model);

    }
}
