using FluentValidation.Results;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Commons.Validators
{
    public class ValidateHelper
    {
        public static async Task<List<Error>> UserRegistrationValidatorError(RecievePaymentDto model)
        {
            var validator = new RecievePaymentValidator();
            var result = await validator.ValidateAsync(model);
            return ErrorsValidated(result);
        }

        public static List<Error> ErrorsValidated(ValidationResult results)
        {
            List<Error> validationErrors = new List<Error>();

            if (!results.IsValid)
            {
                validationErrors.AddRange(results.Errors.Select(error => new Error
                {
                    PropertyName = error.PropertyName,
                    PropertyValue = error.ErrorMessage,
                    HasValidationErrors = true
                }));
            }

            return validationErrors;
        }
    }
}
