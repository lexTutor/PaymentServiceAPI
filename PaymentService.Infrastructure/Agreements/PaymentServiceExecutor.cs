using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Commons.CustomExceptions;
using PaymentService.Application.Commons.Validators;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using PaymentService.Infrastructure.Subsidiaries.SubContracts;
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

        public async Task<Response<PaymentResultDTO>> MakePayment(RecievePaymentDto model)
        {
            var validity = await ValidateHelper.UserRegistrationValidator(model);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }

            Payment paymentModel = _mapper.Map<Payment>(model);
            PaymentStatus paymentStatus = new PaymentStatus { PaymentId = paymentModel.Id, Status = PaymentStatusType.Pending };

            await _UOW.PaymentRepository.AddAsync(paymentModel);
            await _UOW.PaymentStatusRepository.AddAsync(paymentStatus);
            await _UOW.SaveChangesAsync();


            Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();

            if (model.Amount <= 20)
            {
                bool result = await _cheapPaymentGateway.MakeCheapPayment(model);
                response = UpdateAndAssignResult(result, paymentStatus);
            }

            else if (model.Amount <= 500)
            {
                bool result = await _expensivePaymentGateway.MakeExpensivePayment(model);
                response =  UpdateAndAssignResult(result, paymentStatus);
               
            }
            else
            {
                bool result = await _premiumPaymentGateway.MakePremiumPayment(model);
                response =  UpdateAndAssignResult(result, paymentStatus);
            }

            await _UOW.SaveChangesAsync();
            return response;
        }

        private Response<PaymentResultDTO> UpdateAndAssignResult(bool isSuccessful, PaymentStatus paymentStatus)
        {
            Response<PaymentResultDTO> response = new Response<PaymentResultDTO>();
            PaymentResultDTO data = new PaymentResultDTO();
            if (isSuccessful)
            {
                paymentStatus.Status = data.PaymentStatus = PaymentStatusType.Processed;
                 _UOW.PaymentStatusRepository.Update(paymentStatus);

                data.PaymentId = paymentStatus.Id;
                response.IsSuccess = true;
                response.Message = "Transaction Successful";
                response.Data = data;
                return response;
                
            }

            paymentStatus.Status = data.PaymentStatus = PaymentStatusType.Failed;
            _UOW.PaymentStatusRepository.Update(paymentStatus);

            data.PaymentId = paymentStatus.Id;
            response.Message = "Transaction Failed";
            response.Data = data;
            return response;
        }
    }
}
