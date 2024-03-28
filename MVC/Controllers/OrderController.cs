using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.Order;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IIdentityParser<ApplicationUser> _identityParser;

        public OrderController(
            IOrderService orderService,
            IBasketService basketService,
            IIdentityParser<ApplicationUser> identityParser)
        {
            _orderService = orderService;
            _basketService = basketService;
            _identityParser = identityParser;
        }
        public async Task<IActionResult> AddOrder()
        {
            var user = _identityParser.Parse(User);
            string fullName = user.Name;

            string[] parts = fullName.Split(' ');
            string firstName = parts[0];
            string lastName = parts.Length > 1 ? parts[1] : string.Empty;

            var basketItems = await _basketService.GetBasketItems();
            
            decimal totalPrice = 0;

            foreach (var basketItem in basketItems)
            {
                totalPrice += basketItem.Price * basketItem.Amount;
            }

            var orderHistory = new OrderHistory()
            {
                ClientId = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value,
                Name = firstName,
                LastName = lastName,
                TotalPrice = totalPrice,
                BasketItems = basketItems
            };

            await _orderService.AddOrderHistory(orderHistory);
            await _basketService.DeleteAllItemsBasket();

            return RedirectToAction("Index", "Catalog");
        }
        public async Task<IActionResult> Index()
        {
            //var clientId = User.FindFirstValue("sub");

            string clientId = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

            var request = new OrderHistoryRequest()
            {
                ClientId = clientId,
            };

            var order = await _orderService.GetOrderHistory(request);
            if(order == null)
            {
                return View("Error");
            }
            var vm = new OrderViewModel()
            {
                OrderHistories = order.Data
            };

            return View(vm);
        }
    }
}
