using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Vendor.Validators
{
    public class AppCloseValidator:AbstractValidator<dtoAppClose>
    {
        public AppCloseValidator(QueryExecutionMode mode)
        {
            if(mode==QueryExecutionMode.Update)
            {
                RuleFor(p => p.Remark).NotEmpty().WithMessage("Remark is required.").Length(1, 150).WithMessage("Input length must be between 1 and 150 characters.");
                RuleForEach(p => p.AppliIds).NotNull().WithMessage("Select at least one application.").NotEmpty().WithMessage("Select at least one application.");
                RuleFor(p => p.AppliIds).Must(value => { return value.Length > 0; }).WithMessage("Select at least one application.");
            }
            RuleFor(p => p.AppliPrabhagId).Must(value => { return value > 0; }).WithMessage("Prabhag is required.");
            RuleFor(p => p.AppliLocationId).Must(value => { return value > 0; }).WithMessage("Location is required.");
            RuleFor(p => p.AppliHordingId).Must(value => { return value > 0; }).WithMessage("Hording is required.");
        }
    }
}
