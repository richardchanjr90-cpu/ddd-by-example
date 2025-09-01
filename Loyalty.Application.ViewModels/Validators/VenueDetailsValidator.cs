using FluentValidation;

namespace Loyalty.Application.ViewModels.Validators
{
    public class VenueDetailsValidator : AbstractValidator<VenueDetailsViewModel>
    {
        public VenueDetailsValidator()
        {
            RuleFor(x => x.FullDescription)
                .NotEmpty()
                .MaximumLength(4000);

            RuleForEach(x => x.Phones)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(20);

            RuleForEach(x => x.WorkingHours)
                .NotEmpty().WithMessage("Enter working hours.")
                .Must(u => !u.Contains("\"")).WithMessage("Should not contain: \" ")
                .Length(4, 100).WithMessage("Length between 4 and 100 chars.");

            RuleForEach(x => x.PhotosUrl)
                .NotEmpty().WithMessage("Enter working hours.")
                .Must(u => !u.Contains("\"")).WithMessage("Should not contain: \" ")
                .Length(4, 100).WithMessage("Length between 4 and 100 chars.");

            RuleForEach(x => x.PhotosUrl)
                .NotEmpty().WithMessage("Upload at least 1 photo.")
                .Length(4, 200).WithMessage("Length between 4 and 200 chars.")
                .Must(u => !u.Contains("\"")).WithMessage("Should not contain: \" ")
                .Matches(@"[a-z\-\d]").WithMessage("Incorrect format.");
        }
    }
}
