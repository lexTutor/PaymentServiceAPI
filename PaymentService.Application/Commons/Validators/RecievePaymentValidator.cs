using FluentValidation;
using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Commons.Validators
{
    public class RecievePaymentValidator : AbstractValidator<RecievePaymentDto>
    {
        public RecievePaymentValidator()
        {
            RuleFor(model => model.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Amount cannot be empty")
                .GreaterThan(0).WithMessage("Amount must be greater tha  zero");

            RuleFor(model => model.CardHolder)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Cardholder field is required");
                
            RuleFor(model => model.CreditCardNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Credit Card Number field is required")
                .Must(ccn => ccn.Length == 16).WithMessage("Credit Card Number must be exactly 16 digits")
                .Must(ValidNumber).WithMessage("Credit Card Number must be valid");

            RuleFor(model => model.ExpiryDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Expiry date filed is required")
                .Must(ValidDate).WithMessage("Card has expired");

            RuleFor(model => model.SecurityCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("SecurityCode field is required")
                .Must(ccn => ccn.Length == 3).WithMessage("Security Number must be exactly 3 digits");
        }

        private bool ValidDate(DateTime date)
        {
            if (date < DateTime.Now)
            {
                return false;
            }

            return true;
        }

        private bool ValidNumber(string ccn)
        {
            return decimal.TryParse(ccn, out _);
        }
    }
}
