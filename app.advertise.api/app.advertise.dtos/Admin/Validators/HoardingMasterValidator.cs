using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class HoardingMasterValidator : AbstractValidator<dtoHoardingMaster>
    {
        public HoardingMasterValidator(QueryExecutionMode executionMode)
        {
            if (executionMode == QueryExecutionMode.Update)
                RuleFor(p => p.RecordId)
                .NotEmpty().WithMessage("RecordId is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
            RuleFor(p => p.PrabhagId).Must(value => { return value > 0; }).WithMessage("Prabhag is required.");
            RuleFor(p => p.LocationId).Must(value => { return value > 0; }).WithMessage("Location is required.");
            RuleFor(p => p.DisplayTypeId).Must(value => { return value > 0; }).WithMessage("Display Type is required.");
            RuleFor(p => p.Ownership).NotEmpty().WithMessage("Ownership is required.");
            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
            RuleFor(p => p.Landmark)
                .NotEmpty().WithMessage("Landmark is required.")
                .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
            RuleFor(p => p.Building)
                .NotEmpty().WithMessage("Building is required.")
                .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
            RuleFor(p => p.HoardingType)
    .Must(value =>
    {
        return int.TryParse(value, out int intValue) && !(intValue <= 0);
    })
    .WithMessage("Hoarding Type is required.");
        }
    }
}
