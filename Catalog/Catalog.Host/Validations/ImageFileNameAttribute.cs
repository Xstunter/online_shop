#pragma warning disable CS8603

using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Validations
{
    public class ImageFileNameAttribute : ValidationAttribute
    {
        private List<string> _imageFormat = new List<string>() { ".png", ".jpg" };
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !(value is string image))
            {
                return new ValidationResult("Invalid file name");
            }

            foreach (string format in _imageFormat)
            {
                if (image.EndsWith(format, StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("The file name must end with .png or .jpg");
        }
    }
}