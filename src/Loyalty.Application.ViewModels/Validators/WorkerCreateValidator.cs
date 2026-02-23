using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class WorkerCreateValidator : AbstractValidator<CreateWorkerViewModel>
    {
        public WorkerCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(20);

            RuleFor(x => x.WorkerId)
                .MinimumLength(2)
                .MaximumLength(200)
                .NotEmpty();

            //RuleFor(x => x.PositionName)
            //    .NotEmpty();

            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.Role).LessThanOrEqualTo((int)VenueUserRole.Owner)
                .WithMessage("Must be in range of Enum values");
        }
    }
}
