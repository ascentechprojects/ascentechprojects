using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Vendor.Validators
{
    public class OTPPasswordResetValidator : AbstractValidator<dtoOTPPasswordReset>
    {
        public OTPPasswordResetValidator()
        {
            RuleFor(p => p.OTP).Must(value => { return StaticHelpers.InputValidator(value, ValidatorType.OnlyNumber); }).WithMessage("Invalid OTP.");
            RuleFor(x => x.Password).NotNull().WithMessage("Password is required.").Length(3, 50).WithMessage("Password Must be between 3 and 50 characters long.");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Confirm password is required.").Equal(x => x.Password).WithMessage("Passwords must match");
        }
    }
}
