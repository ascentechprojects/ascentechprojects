using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Vendor.Validators
{
    public class CitizenValidator : AbstractValidator<dtoCitizen>
    {
        public CitizenValidator()
        {
            RuleFor(p => p.UlbId).Must(value => { return value > 0; }).WithMessage("Ulb is required.");
            RuleFor(p => p.FName).NotEmpty().WithMessage("First Name is required.").Length(1, 100).WithMessage("Input length must be between 1 and 100 characters.");
            RuleFor(p => p.MName).NotEmpty().WithMessage("Middle Name is required.").Length(1, 100).WithMessage("Input length must be between 1 and 100 characters.");
            RuleFor(p => p.LName).NotEmpty().WithMessage("Last Name is required.").Length(1, 100).WithMessage("Input length must be between 1 and 100 characters.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Address is required.").Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
            RuleFor(p => p.EmailId).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.Email); }).WithMessage("Invalid Email.");
            RuleFor(p => p.MobileNo).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.MobileNo); }).WithMessage("Invalid Mobile number.");
            RuleFor(p => p.Gender).Must(value => { return StaticHelpers.ValidGenders().Keys.Any(x => string.Equals(x, value, StringComparison.OrdinalIgnoreCase)); }).WithMessage("Invalid Gender.");
            RuleFor(p => p.AadharNo).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.AAdhaar); }).WithMessage("Invalid Aadhar number.");
            RuleFor(p => p.DateOfBirth).Must(value => { return StaticHelpers.ValidateDate(value) && value < DateTime.Now; }).WithMessage("Date of Birth is required.");

        }
    }
}
