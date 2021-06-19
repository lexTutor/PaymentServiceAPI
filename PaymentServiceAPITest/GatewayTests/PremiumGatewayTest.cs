using PaymentService.Domain.DataTransfer;
using PaymentService.Infrastructure.Gateways.SubAgreements;
using PaymentServiceAPITest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentServiceAPITest
{
    public class PremiumGatewayTest
    {
        [Fact]
        public void PremiumPaymentGateway_Should_Return_GatewayResult()
        {
            //Arrange
            var premiumGateway = new PremiumPaymentGateway();

            //Act
            var actual = premiumGateway.MakePremiumPayment(PremiumModelHelper.PremiumPaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<GatewayResult>(actual);
        }
    }
}
