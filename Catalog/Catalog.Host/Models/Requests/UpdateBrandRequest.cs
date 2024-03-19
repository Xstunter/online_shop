#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class UpdateBrandRequest
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }
    }
}
