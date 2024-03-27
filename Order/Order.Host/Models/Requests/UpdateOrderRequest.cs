using Order.Host.Data.Entities;
using Order.Host.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests
{
    public class UpdateOrderRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Range(0, 4, ErrorMessage = "0 = Pending, 1 = Processing, 2 = Shipped, 3 = Delivered, 4 = Cancelled")]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus orderStatus { get; set; }
    }
}
