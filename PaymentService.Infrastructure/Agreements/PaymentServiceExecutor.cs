using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Commons.CustomExceptions;
using PaymentService.Application.Commons.Validators;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Gateways.SubContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Agreements
{
    public class PaymentServiceExecutor : IPaymentService
    {
        private readonly IPremiumPaymentGateway _premiumPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        public PaymentServiceExecutor(IServiceProvider serviceProvider)
        {
            _premiumPaymentGateway = serviceProvider.GetRequiredService<IPremiumPaymentGateway>();
            _expensivePaymentGateway = serviceProvider.GetRequiredService<IExpensivePaymentGateway>();
            _cheapPaymentGateway = serviceProvider.GetRequiredService<ICheapPaymentGateway>();
            _UOW = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        /// <summary>
        /// Enables Calls the external service to make the required payments
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<PaymentResultDTO>> MakePayment(RecievePaymentDto model)
        {
            var validity = await ValidateHelper.RecievePaymentValidator(model);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }

            Payment paymentModel = _mapper.Map<Payment>(model);
            PaymentStatus paymentStatus = new PaymentStatus();
            paymentStatus.PaymentId = paymentModel.Id;

            await _UOW.PaymentRepository.AddAsync(paymentModel);
            await _UOW.PaymentStatusRepository.AddAsync(paymentStatus);
            await _UOW.SaveChangesAsync();


            Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();

            if (model.Amount <= 20)
            {
                GatewayResult result =  _cheapPaymentGateway.MakeCheapPayment(model);
                response = UpdateAndAssignResult(result, paymentStatus, paymentModel);
            }

            else if (model.Amount <= 500)
            {
                GatewayResult result = ExpensivePayment(model);
                response =  UpdateAndAssignResult(result, paymentStatus, paymentModel);
               
            }
            else
            {
                GatewayResult result = PremiumPayment(model);
                response =  UpdateAndAssignResult(result, paymentStatus, paymentModel);
            }

            await _UOW.SaveChangesAsync();

            return response;
        }

        /// <summary>
        /// Calls the expensive payment service and returns the generated result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private GatewayResult ExpensivePayment(RecievePaymentDto model) 
        {
            var result =  _expensivePaymentGateway.MakeExpensivePayment(model);

            if (!result.Status)
            {
                result =  _cheapPaymentGateway.MakeCheapPayment(model);
            }
            return result; 
        }

        /// <summary>
        /// Calls the premium Payment service and returns the generated result.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private GatewayResult PremiumPayment(RecievePaymentDto model) 
        {
            GatewayResult result = default;
            bool status = false;
            int count = 0;

            while (!status && count <= 2)
            {
                result = _premiumPaymentGateway.MakePremiumPayment(model);
                status = result.Status;
                count += 1;
            }

            return result;
        }

        /// <summary>
        /// Updates the Payment and PaymentStatus tables and creates the response object to be returned from the result of the external service call
        /// </summary>
        /// <param name="gatewayResult"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        private Response<PaymentResultDTO> UpdateAndAssignResult(GatewayResult gatewayResult, PaymentStatus paymentStatus, Payment payment)
        {
            Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();
            PaymentResultDTO resultData = new PaymentResultDTO();

            payment.PymentResultId = gatewayResult.Id;
            _UOW.PaymentRepository.Update(payment);

            paymentStatus.Status = gatewayResult.Status ? PaymentStatusType.Processed : PaymentStatusType.Failed;
            _UOW.PaymentStatusRepository.Update(paymentStatus);

            resultData.PaymentStatus = gatewayResult.Status? PaymentStatusType.Processed.ToString(): PaymentStatusType.Failed.ToString();
            resultData.PaymentId = payment.Id;
            resultData.AmountPaid = payment.Amount;

            response.Data = resultData;
            response.IsSuccess = gatewayResult.Status;
            response.Message = gatewayResult.Status? "Transaction Successful" : "Transaction Failed";

            return response;
        }
    }
}
