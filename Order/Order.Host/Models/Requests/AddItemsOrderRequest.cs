using Order.Host.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static ServiceStack.LicenseUtils;

namespace Order.Host.Models.Requests
{
    public class AddItemsOrderRequest
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Not negative numbers")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Not negative numbers")]
        public int Amount { get; set; }
    }
}
