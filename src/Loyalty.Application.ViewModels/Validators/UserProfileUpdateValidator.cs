using FluentValidation;
using Loyalty.Application.ViewModels.UserProfile;

namespace Loyalty.Application.ViewModels.Validators
{
    public class UserProfileUpdateValidator : AbstractValidator<UserProfileViewModel>
    {
        public UserProfileUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PositionName)
                .MaximumLength(100);
        }
    }
}
