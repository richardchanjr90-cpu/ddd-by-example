using FluentValidation;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Application.ViewModels.Validators
{
    public class WorkerCreateValidator : AbstractValidator<WorkerViewModel>
    {
        public WorkerCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(20);

            RuleFor(x => x.Role).LessThanOrEqualTo((int)VenueUserRole.Owner)
                .WithMessage("Must be in range of Enum values");

            RuleFor(x => x.PositionName)
                .NotEmpty();
        }
    }
}
