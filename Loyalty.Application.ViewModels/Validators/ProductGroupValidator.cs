using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Product;
using Loyalty.Application.ViewModels.ProductGroup;

namespace Loyalty.Application.ViewModels.Validators
{
    public class ProductGroupValidator : AbstractValidator<ProductGroupViewModel>
    {
        public ProductGroupValidator()
        {
            RuleFor(x => x.Icon)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
