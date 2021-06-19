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
    public class ExpensiveGatewayTest
    {
        [Fact]
        public void ExpensivePaymentGateway_Should_Return_GatewayResult()
        {
            //Arrange
            var expensiveGateway = new ExpensivePaymentGateway();

            //Act
            var actual = expensiveGateway.MakeExpensivePayment(ExpensiveModelHelper.ExpensivePaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.IsType<GatewayResult>(actual);
        }
    }
}
