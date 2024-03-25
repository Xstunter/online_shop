using System.Text.Json.Serialization;

namespace Order.Host.Data.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int OrderHistoryId { get; set; }
        [JsonIgnore]
        public OrderHistory OrderHistory { get; set; }
    }
}
