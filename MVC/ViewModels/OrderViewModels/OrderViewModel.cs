using MVC.ViewModels.Order;

namespace MVC.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<OrderHistory> OrderHistories { get; set; }
    }
}
