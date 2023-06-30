using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class HoardingMasterValidator : AbstractValidator<dtoHoardingMaster>
    {
        public HoardingMasterValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(p => p.PrabhagId).LessThanOrEqualTo(0).WithMessage("Prabhag is required.");
            RuleFor(p => p.LocationId).LessThanOrEqualTo(0).WithMessage("Location is required.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(p => p.DisplayTypeId).LessThanOrEqualTo(0).WithMessage("Display Type is required.");
            RuleFor(p => p.Landmark).NotEmpty().WithMessage("Landmark is required.");
            RuleFor(p => p.HoardingType)
    .Must(value =>
    {
        return int.TryParse(value, out int intValue) && !(intValue <= 0);
    })
    .WithMessage("Hoarding Type is required.");
        }
    }
}
