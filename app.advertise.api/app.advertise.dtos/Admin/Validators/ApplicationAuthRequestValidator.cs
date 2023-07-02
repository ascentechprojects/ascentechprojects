using app.advertise.libraries;
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

        public ApplicationAuthRequestValidator(dtoApplicationAuthRequest authRequest)
        {
            RuleFor(model => model.Remark).NotEmpty().WithMessage("Remark is required.");
            RuleFor(p => p.StatusFlag)
            .Must(value =>
             {
                return StaticHelpers.RemarkStatus().ContainsValue(value);
              }).WithMessage("Invalid status flag.");

            RuleFor(p => p.AppliId)
            .Must(value =>
            {
                return !(value <= 0);
            }).WithMessage("Application Id is required.");
           
        }
    }
}
