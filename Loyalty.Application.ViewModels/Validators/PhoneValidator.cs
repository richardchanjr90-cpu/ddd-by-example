using System;
using FluentValidation;
using PhoneNumbers;

namespace Loyalty.Application.ViewModels.Validators
{
    public class PhoneValidator : AbstractValidator<string>
    {
        public PhoneValidator()
        {
            RuleFor(x => x)
                .Must(BeValidPhone)
                .WithMessage(x => $"Phone {x} should be in a valid format: (E.164 Phone Number Formatting)");
        }

        private bool BeValidPhone(string phoneString)
        {
            bool isValid = true;
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneString, "BY");
            }
            catch (Exception e)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
