using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Vendor.Validators
{
    public class ApplicationDetailsValidator : AbstractValidator<dtoApplicationDetails>
    {
        public ApplicationDetailsValidator(QueryExecutionMode mode)
        {
            if(mode== QueryExecutionMode.Insert)
                RuleFor(ac => ac.AppliFromDate).GreaterThanOrEqualTo(ac => DateTime.Now).WithMessage("From date must >= Today's date");

            if(mode== QueryExecutionMode.Update)
                RuleFor(p => p.RecordId).NotEmpty().WithMessage("RecordId is required.");

            RuleFor(p => p.AppliPrabhagId).Must(value => { return value > 0; }).WithMessage("Prabhag is required.");
            RuleFor(p => p.AppliLocationId).Must(value => { return value > 0; }).WithMessage("Location is required.");
            RuleFor(p => p.AppliHordingId).Must(value => { return value > 0; }).WithMessage("Hording is required.");
            RuleFor(p => p.AppliAppName).NotEmpty().WithMessage("Name is required.").Length(1, 150).WithMessage("Input length must be between 1 and 150 characters.");
            RuleFor(p => p.AppliLicenseOutNo).NotEmpty().WithMessage("Name is required.").Length(1, 150).WithMessage("Input length must be between 1 and 150 characters.");
            RuleFor(p => p.AppliAddress).NotEmpty().WithMessage("Address is required.").Length(1, 150).WithMessage("Input length must be between 1 and 150 characters.");
            RuleFor(p => p.AppliEmail).Must(value => {return StaticHelpers.InputValidator(value, ValidatorType.Email); }).WithMessage("Invalid Email.");
            RuleFor(p => p.AppliMobileNo).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.MobileNo); }).WithMessage("Invalid Mobile number.");
            RuleFor(ac => ac.AppliFromDate).NotEmpty().WithMessage("From Date is required");
            RuleFor(m => m.AppliUpToDate).NotEmpty().WithMessage("Upto date is required").GreaterThan(m => m.AppliFromDate).WithMessage("Upto date must after Start date");
            RuleFor(m => m.Quantity).NotEmpty().WithMessage("Quantity is required").GreaterThan(m => 0).WithMessage("Quantity is required");
        }   

    }

}
