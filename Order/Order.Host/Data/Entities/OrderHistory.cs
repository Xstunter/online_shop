using Order.Host.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Data.Entities
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus OrderStatus { get; set; }
        public List<BasketItem> BasketItems { get; set; }

    }
}
