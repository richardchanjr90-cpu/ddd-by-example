using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class VenueWorkerValidator: AbstractValidator<VenueWorkerViewModel>
    {
        public VenueWorkerValidator()
        {
            RuleFor(x => x.PositionName)
                .NotEmpty();

            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.Role).LessThanOrEqualTo((int)VenueUserRole.Owner)
                .WithMessage("Must be in range of Enum values");
        }
    }
}
