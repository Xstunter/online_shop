#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateBrandRequest
    {
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }
    }
}
