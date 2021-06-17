using PaymentService.Domain.DataTransfer;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Gateways.SubContracts
{
    /// <summary>
    /// Payment Contract for expensive payments between the range of $22 - $500
    /// </summary>
    public interface IExpensivePaymentGateway
    {
        GatewayResult MakeExpensivePayment(RecievePaymentDto model);

    }
}
