using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Vendor.Validators
{
    public class CitizenLoginRequestValidator : AbstractValidator<dtoCitizenLoginRequest>
    {
        public CitizenLoginRequestValidator()
        {
            RuleFor(p => p.UserId).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.Email); }).WithMessage("Invalid Email.");
            RuleFor(p => p.SecretKey).NotEmpty().WithMessage("Password is required.").Length(1, 150).WithMessage("Input length must be between 1 and 150 characters.");
            RuleFor(p => p.UlbId).Must(value => { return value > 0; }).WithMessage("Ulb is required.");
        }
    }
}
