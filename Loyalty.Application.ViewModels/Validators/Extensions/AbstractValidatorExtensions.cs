using System;
using FluentValidation;

namespace Loyalty.Application.ViewModels.Validators.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static bool BeValidGuid<T>(this AbstractValidator<T> validator, string guidString)
        {
            return Guid.TryParse(guidString, out var result) && Guid.Empty != result;
        }
    }
}
