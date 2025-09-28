using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Product;

namespace Loyalty.Application.ViewModels.Validators
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Icon)
                .NotEmpty();

            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
