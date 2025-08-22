using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Loyalty.Core.ViewModels.Validators
{
    public class PublishedVenueValidator : AbstractValidator<VenueViewModel>
    {
        public PublishedVenueValidator()
        {
            RuleFor(x => x.CategoryType)
                .GreaterThan(0);

            RuleFor(x => x.Location)
                .NotNull()
                .SetValidator(new LocationValidator());

            RuleFor(x => x.Details)
                .NotNull()
                .SetValidator(new VenueDetailsValidator());

            RuleFor(x => x.LogoUrl)
                .NotEmpty().WithMessage("Enter url.")
                .Length(4, 30).WithMessage("Length between 4 and 30 chars.")
                .Matches(@"[a-z\-\d]").WithMessage("Incorrect format.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
