#pragma warning disable CS8603

using System.ComponentModel.DataAnnotations;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Validations
{
    public class CountQuantityBrandIdsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetRequiredService<IServiceProvider>();
            var catalogBrandService = serviceProvider.GetRequiredService<ICatalogBrandService>();

            if (value == null || !(value is int count))
            {
                return new ValidationResult("Invalid name");
            }

            return Task.Run(async () =>
            {
                var ids = await catalogBrandService.GetBrandIdsAsync();

                if (ids.Contains(count))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult($"Not found brand id: {value}");
            }).Result;
        }
    }
}