using AutoMapper;
using Moq;
using PaymentService.Application.Commons.CustomExceptions;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Agreements;
using PaymentService.Infrastructure.Gateways.SubContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentServiceAPITest
{
    public class PaymentServiceTestForErrors
    {
        private IServiceProvider serviceProvider;
        private readonly Mock<IMapper> MockAutoMapper = new Mock<IMapper>();
        private readonly Mock<IUnitOfWork> MockUOW = new Mock<IUnitOfWork>();
        private readonly Mock<ICheapPaymentGateway> MockCheapPaymentGt = new Mock<ICheapPaymentGateway>();
        private readonly Mock<IExpensivePaymentGateway> MockExpensiveGt = new Mock<IExpensivePaymentGateway>();
        private readonly Mock<IPremiumPaymentGateway> MockPremiumGt = new Mock<IPremiumPaymentGateway>();

        public PaymentServiceTestForErrors()
        {
            var mockServiceProvider = new Mock<IServiceProvider>(MockBehavior.Strict);
            mockServiceProvider.Setup(x => x.GetService(typeof(IMapper))).Returns(MockAutoMapper.Object).Verifiable();
            mockServiceProvider.Setup(x => x.GetService(typeof(IUnitOfWork))).Returns(MockUOW.Object).Verifiable();
            mockServiceProvider.Setup(x => x.GetService(typeof(ICheapPaymentGateway))).Returns(MockCheapPaymentGt.Object).Verifiable();
            mockServiceProvider.Setup(x => x.GetService(typeof(IExpensivePaymentGateway))).Returns(MockExpensiveGt.Object).Verifiable();
            mockServiceProvider.Setup(x => x.GetService(typeof(IPremiumPaymentGateway))).Returns(MockPremiumGt.Object).Verifiable();
            serviceProvider = mockServiceProvider.Object;
        }

        [Fact]
        public async Task PaymentService_Should_throw_an_error_due_to_invalidAmount()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);

            //Act
            var actual =  await Assert.ThrowsAsync<BadRequestException>
                (() => paymentService.MakePayment(ErrorModelHelper.GetPaymentErrorModelForAmount()));

            //Assert
            Assert.NotNull(actual.Errors.Select(c => c.PropertyName == "Amount"));
            
        }

        [Fact]
        public async Task PaymentService_Should_throw_an_error_due_to_invalidSecurityNumber()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);

            //Act
            var actual = await Assert.ThrowsAsync<BadRequestException>
                (() => paymentService.MakePayment(ErrorModelHelper.GetPaymentErrorModelForSecurityCode()));

            //Assert
            Assert.NotNull(actual.Errors.Select(c => c.PropertyName == "SecurityCode"));

        }

        [Fact]
        public async Task PaymentService_Should_throw_an_error_due_to_invalid_ExpiryDate()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);

            //Act
            var actual = await Assert.ThrowsAsync<BadRequestException>
                (() => paymentService.MakePayment(ErrorModelHelper.GetPremiumPaymentErrorModelForExpiryDate()));

            //Assert
            Assert.NotNull(actual.Errors.Select(c => c.PropertyName == "ExpiryDate"));

        }

        [Fact]
        public async Task PaymentService_Should_throw_an_error_due_to_invalid_CreditCardNumber()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);

            //Act
            var actual = await Assert.ThrowsAsync<BadRequestException>
                (() => paymentService.MakePayment(ErrorModelHelper.GetPremiumPaymentErrorModelCreditCardNumber()));

            //Assert
            Assert.NotNull(actual.Errors.Select(c => c.PropertyName == "CreditCardNumber"));

        }
   }
}
