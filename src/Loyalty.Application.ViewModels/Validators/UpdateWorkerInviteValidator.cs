using FluentValidation;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class UpdateWorkerInviteValidator : AbstractValidator<UpdateInviteViewModel>
    {
        public UpdateWorkerInviteValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Role)
                .LessThanOrEqualTo((int)VenueUserRole.Owner)
                .WithMessage("Must be in range of Enum values");

            RuleFor(x => x.PositionName)
                .NotEmpty();
        }
    }
}
