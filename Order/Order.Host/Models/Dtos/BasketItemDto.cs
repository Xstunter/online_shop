namespace Order.Host.Models.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
