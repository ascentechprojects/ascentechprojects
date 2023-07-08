using FluentValidation;
using System.Text.RegularExpressions;

namespace app.advertise.dtos.Admin.Validators
{
    public class LocationMasterValidator : AbstractValidator<dtoLocationMaster>
    {
        public LocationMasterValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
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
        return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^\d{6}$"); ;
    })
    .WithMessage("Invalid Pincode.");
        }
    }
}
