#pragma warning disable CS8603

using System.ComponentModel.DataAnnotations;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Validations
{
    public class CountQuantityTypeIdsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var serviceProvider = validationContext.GetRequiredService<IServiceProvider>();
            var catalogTypeService = serviceProvider.GetRequiredService<ICatalogTypeService>();

            if (value == null || !(value is int count))
            {
                return new ValidationResult("Invalid name");
            }

            return Task.Run(async () =>
            {
                var ids = await catalogTypeService.GetTypeIdsAsync();

                if (ids.Contains(count))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult($"Not found type id: {value}");
            }).Result;
        }
    }
}