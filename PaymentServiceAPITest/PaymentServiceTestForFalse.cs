using AutoMapper;
using Moq;
using PaymentService.Application.Contracts;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Agreements;
using PaymentService.Infrastructure.Gateways.SubContracts;
using PaymentServiceAPITest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentServiceAPITest
{
    public class PaymentServiceTestForFalse
    {
        private IServiceProvider serviceProvider;
        private readonly Mock<IMapper> MockAutoMapper = new Mock<IMapper>();
        private readonly Mock<IUnitOfWork> MockUOW = new Mock<IUnitOfWork>();
        private readonly Mock<ICheapPaymentGateway> MockCheapPaymentGt = new Mock<ICheapPaymentGateway>();
        private readonly Mock<IExpensivePaymentGateway> MockExpensiveGt = new Mock<IExpensivePaymentGateway>();
        private readonly Mock<IPremiumPaymentGateway> MockPremiumGt = new Mock<IPremiumPaymentGateway>();
        public PaymentServiceTestForFalse()
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
        public async Task PaymentService_Should_Retrun_Success_And_use_the_CheapPaymentGateway()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);
            Construct(CheapModelHelper.CheapGatewayReturnFalse(), CheapModelHelper.CheapPaymentModel(), CheapModelHelper.ReturnCheapPaymentDTO());
            var expected = "Transaction Failed";

            //Act
            var actual = await paymentService.MakePayment(CheapModelHelper.CheapPaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.False(actual.IsSuccess);
            Assert.Equal(actual.Data.AmountPaid, CheapModelHelper.CheapPaymentModel().Amount);
            Assert.Equal(expected, actual.Message);


        }

        [Fact]
        public async Task PaymentService_Should_Retrun_Success_And_use_the_ExpensivePaymentGateway()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);
            Construct(ExpensiveModelHelper.ExpensiveGatewayResultFalse(), ExpensiveModelHelper.ExpensivePaymentModel(), ExpensiveModelHelper.ReturnExpensivePaymentDTO());
            var expected = "Transaction Failed";

            //Act
            var actual = await paymentService.MakePayment(ExpensiveModelHelper.ExpensivePaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.False(actual.IsSuccess);
            Assert.Equal(actual.Data.AmountPaid, ExpensiveModelHelper.ExpensivePaymentModel().Amount);
            Assert.Equal(expected, actual.Message);
        }

        [Fact]
        public async Task PaymentService_Should_Retrun_Success_And_use_the_PremiumPaymentGateway()
        {
            //Arrange
            var paymentService = new PaymentServiceExecutor(serviceProvider);
            Construct(PremiumModelHelper.PremiumGatewayResultFalse(), PremiumModelHelper.PremiumPaymentModel(), PremiumModelHelper.ReturnPremiumPaymentDTO());
            var expected = "Transaction Failed";

            //Act
            var actual = await paymentService.MakePayment(PremiumModelHelper.PremiumPaymentDTO());

            //Assert
            Assert.NotNull(actual);
            Assert.False(actual.IsSuccess);
            Assert.Equal(actual.Data.AmountPaid, PremiumModelHelper.PremiumPaymentModel().Amount);
            Assert.Equal(expected, actual.Message);

        }

        private void Construct(GatewayResult gatewayResult, Payment payment, PaymentResultDTO result)
        {
            MockUOW.Setup(prop => prop.PaymentRepository.AddAsync(It.IsAny<Payment>())).Returns(Task.FromResult(Task.CompletedTask));
            MockUOW.Setup(prop => prop.PaymentStatusRepository.AddAsync(It.IsAny<PaymentStatus>())).Returns(Task.FromResult(Task.CompletedTask));
            MockUOW.Setup(prop => prop.PaymentRepository.Update(It.IsAny<Payment>()));
            MockUOW.Setup(prop => prop.PaymentStatusRepository.Update(It.IsAny<PaymentStatus>()));
            MockUOW.Setup(prop => prop.SaveChangesAsync()).Returns(Task.FromResult(true));

            MockCheapPaymentGt.Setup(method => method.MakeCheapPayment(It.IsAny<RecievePaymentDto>())).Returns(gatewayResult);
            MockExpensiveGt.Setup(method => method.MakeExpensivePayment(It.IsAny<RecievePaymentDto>())).Returns(gatewayResult);
            MockPremiumGt.Setup(method => method.MakePremiumPayment(It.IsAny<RecievePaymentDto>())).Returns(gatewayResult);

            MockAutoMapper.Setup(method => method.Map<Payment>(It.IsAny<RecievePaymentDto>())).Returns(payment);
            MockAutoMapper.Setup(method => method.Map<PaymentResultDTO>(It.IsAny<Payment>())).Returns(result);
        }
    }
}
