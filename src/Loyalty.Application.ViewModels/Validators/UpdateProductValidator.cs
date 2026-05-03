using System;
using FluentValidation;
using Loyalty.Application.ViewModels.Product;

namespace Loyalty.Application.ViewModels.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductValidator()
        {
            //RuleFor(x => x.Icon)
            //    .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
