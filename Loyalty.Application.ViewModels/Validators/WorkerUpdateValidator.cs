using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class WorkerUpdateValidator : AbstractValidator<WorkerViewModel>
    {
        public WorkerUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(20);

            RuleFor(x => x.WorkerId)
                .MinimumLength(2)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.PositionName)
                .NotEmpty();

            RuleFor(x => x.Role).LessThanOrEqualTo((int)VenueUserRole.Owner)
                .WithMessage("Must be in range of Enum values");

            RuleFor(x => x.PhotoUri)
                .NotEmpty().WithMessage("Enter photo.")
                .Length(4, 200).WithMessage("Length between 4 and 200 chars.")
                .Matches(@"[a-z\-\d]").WithMessage("Incorrect format.");
        }
    }
}
