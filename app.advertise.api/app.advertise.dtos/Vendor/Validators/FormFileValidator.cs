using app.advertise.libraries;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace app.advertise.dtos.Vendor.Validators
{
    public class FormFileValidator:AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(file => file).NotNull().WithMessage("File is required.");
            RuleFor(file => file.Length).GreaterThan(0).WithMessage("File length must be greater than 0.").LessThanOrEqualTo(10 * 1024 * 1024).WithMessage("File size must be less than 10 MB.");
            RuleFor(file => file.FileName).Must(value=>StaticHelpers.AllowedFileExtensions().Contains(value.Split('.').Last(),StringComparer.OrdinalIgnoreCase)).WithMessage("Invalid file type. Only JPG,JPEG,PNG files are allowed.");
            RuleFor(p => p.ContentType).Must(value => { return value.Contains("image",StringComparison.OrdinalIgnoreCase); }).WithMessage("Image type is required.");
        }
    }
}
