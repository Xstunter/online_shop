using System.ComponentModel.DataAnnotations;
using Catalog.Host.Validations;

namespace Catalog.Host.Models.Requests;

public class CreateProductRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(255)]
    public string Description { get; set; } = null!;
    [Required]
    [Range(0, 10000, ErrorMessage = "You must enter numbers 0 to 10000")]
    public decimal Price { get; set; }
    [Required]
    [ImageFileName]
    public string PictureFileName { get; set; } = null!;
    [Required]
    [CountQuantityTypeIds]
    public int CatalogTypeId { get; set; }
    [Required]
    [CountQuantityBrandIds]
    public int CatalogBrandId { get; set; }
    [Range(0, 1000, ErrorMessage = "You must enter numbers 0 to 1000")]
    public int AvailableStock { get; set; }
}