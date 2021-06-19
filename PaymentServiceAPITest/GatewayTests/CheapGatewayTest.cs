using PaymentService.Domain.DataTransfer;
using PaymentService.Infrastructure.Gateways.SubAgreements;
using Xunit;

namespace PaymentServiceAPITest
{
    public class CheapGatewayTest
    {
        [Fact]
        public void CheapPaymentGateway_Should_Return_GatewayResult()
        {
            //Arrange
            var cheapGateway = new CheapPaymentGateway();

            //Act
            var actual = cheapGateway.MakeCheapPayment(CheapModelHelper.CheapPaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<GatewayResult>(actual);
        }
    }
}
