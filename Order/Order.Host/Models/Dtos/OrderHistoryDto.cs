namespace Order.Host.Models.Dtos
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketItemDto> BasketItem { get; set; }

    }
}
