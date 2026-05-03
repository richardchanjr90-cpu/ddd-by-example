using System;
using FluentValidation;
using Loyalty.Application.ViewModels.Product;

namespace Loyalty.Application.ViewModels.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            //RuleFor(x => x.Icon)
            //    .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
