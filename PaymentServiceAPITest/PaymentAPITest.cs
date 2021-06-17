using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using PaymentServiceAPI.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PaymentServiceAPITest
{
    public class PaymentAPITest
    {
        private IServiceProvider serviceProvider;
        private readonly Mock<IPaymentService> MockPaymentService = new Mock<IPaymentService>();
        public PaymentAPITest()
        {
            var mockServiceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
            mockServiceProvider.Setup(x => x.GetService(typeof(IPaymentService))).Returns(MockPaymentService.Object).Verifiable();

            serviceProvider = mockServiceProvider.Object;
        }
        [Fact]
        public async Task Payment_API_Should_Return_Ok()
        {
            //Arrange
            Construct(true);
            var paymentController = new PaymentController(serviceProvider);
            RecievePaymentDto payment = new RecievePaymentDto();

            //Act
            var actual =  await paymentController.MakePayment(payment) as OkObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Value);
            Assert.Equal(StatusCodes.Status200OK, actual.StatusCode);
        }

        [Fact]
        public async Task Payment_API_Should_Return_BadRequest()
        { 
            //Arrange
            Construct(false);
            var paymentController = new PaymentController(serviceProvider);
            RecievePaymentDto payment = new RecievePaymentDto();

            //Act
            var actual = await paymentController.MakePayment(payment) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, actual.StatusCode);

        }

        private void Construct(bool isSuccess)
        {

            MockPaymentService.Setup(method => method.MakePayment(It.IsAny<RecievePaymentDto>()))
                .Returns(Task.FromResult(new Response<PaymentResultDTO> {IsSuccess = isSuccess}));
        }
    }
}
