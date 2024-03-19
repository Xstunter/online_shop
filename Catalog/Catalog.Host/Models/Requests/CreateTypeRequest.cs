#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class CreateTypeRequest
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
    }
}
