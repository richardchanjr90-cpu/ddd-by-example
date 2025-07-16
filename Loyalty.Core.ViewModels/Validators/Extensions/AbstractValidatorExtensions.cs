using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Loyalty.Core.ViewModels.Validators.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static bool BeValidGuid<T>(this AbstractValidator<T> validator, string guidString)
        {
            return Guid.TryParse(guidString, out Guid result) && Guid.Empty != result;
        }
    }
}
