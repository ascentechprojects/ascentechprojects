using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class LocationMasterValidator : AbstractValidator<dtoLocationMaster>
    {
        public LocationMasterValidator(QueryExecutionMode executionMode)
        {
            if (executionMode == QueryExecutionMode.Update)
                RuleFor(p => p.RecordId)
                .NotEmpty().WithMessage("RecordId is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters."); ;
            RuleFor(p => p.Area).Must(value =>
            {
                return (value > 0);
            })
    .WithMessage("Area is required.");
            RuleFor(p => p.PrabhagId).Must(value =>
    {
        return (value > 0);
    })
    .WithMessage("Prabhag is required.");

            RuleFor(p => p.PinCode).Must(value =>
            {
        return StaticHelpers.InputValidator(value, ValidatorType.Pincode);
    })
    .WithMessage("Invalid Pincode.");
        }
    }
}
