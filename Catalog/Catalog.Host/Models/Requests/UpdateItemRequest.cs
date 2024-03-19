using System.ComponentModel.DataAnnotations;
using Catalog.Host.Validations;

namespace Catalog.Host.Models.Requests
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
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
        [ImageFileName(ErrorMessage = "The file name must end with .png")]
        public string PictureFileName { get; set; } = null!;
        [Required]
        [Range(1, 4, ErrorMessage = "Not 0 or negetive number")]
        public int CatalogTypeId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Not 0 or negetive number")]
        public int CatalogBrandId { get; set; }
        [Range(0, 1000, ErrorMessage = "You must enter numbers 0 to 1000")]
        public int AvailableStock { get; set; }
    }
}
