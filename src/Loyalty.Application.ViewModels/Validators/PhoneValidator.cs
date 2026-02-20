using System;
using System.Text.RegularExpressions;
using FluentValidation;

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
                if (phoneString.StartsWith("+"))
                {
                    phoneString = phoneString.TrimStart('+');
                    var number = Int64.Parse(phoneString);
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
