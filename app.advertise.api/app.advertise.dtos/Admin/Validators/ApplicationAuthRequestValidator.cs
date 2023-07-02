using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class ApplicationAuthRequestValidator : AbstractValidator<dtoApplicationAuthRequest>
    {
        public ApplicationAuthRequestValidator()
        {
            RuleFor(p => p.PrabhagId)
    .Must(value =>
    {
        return !(value <= 0);
    })
    .WithMessage("Prabhag is required.");

            RuleFor(p => p.LocationId)
   .Must(value =>
   {
       return !(value <= 0);
   })
   .WithMessage("Location is required.");

        }
    }
}
