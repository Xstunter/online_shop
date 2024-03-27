using MVC.ViewModels.Basket;

namespace MVC.ViewModels.Order
{
    public class OrderHistory
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; } = "Pending";
        public List<BasketItems> BasketItems { get; set; }
    }
}
