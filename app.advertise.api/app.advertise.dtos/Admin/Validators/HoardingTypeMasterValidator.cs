using app.advertise.libraries;
using FluentValidation;

namespace app.advertise.dtos.Admin.Validators
{
    public class HoardingTypeMasterValidator : AbstractValidator<dtoHoardingtypeMaster>
    {
        public HoardingTypeMasterValidator(QueryExecutionMode executionMode)
        {
            if (executionMode == QueryExecutionMode.Update)
                RuleFor(p => p.RecordId)
                .NotEmpty().WithMessage("RecordId is required.");


            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Name is required.")
               .Length(1, 200).WithMessage("Input length must be between 1 and 200 characters.");
        }
    }
}
