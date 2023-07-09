using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class AuthRequestValidator:AbstractValidator<dtoAuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(p => p.User).NotEmpty().WithMessage("User is required.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
