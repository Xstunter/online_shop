using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests
{
    public class AddOrderRequest
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0, 10000, ErrorMessage = "Not negative numbers")]
        public decimal TotalPrice { get; set; }
        [Required]
        public List<AddItemsOrderRequest> BasketItems { get; set; }
    }
}
