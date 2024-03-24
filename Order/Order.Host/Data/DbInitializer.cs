using Order.Host.Data.Entities;

namespace Order.Host.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.OrderHistories.Any())
            {
                await context.OrderHistories.AddRangeAsync(GetPreconfiguredOrderHistories());

                await context.SaveChangesAsync();
            }

            if (!context.BasketItems.Any())
            {
                await context.BasketItems.AddRangeAsync(GetPreconfiguredBasketItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<OrderHistory> GetPreconfiguredOrderHistories()
        {
            return new List<OrderHistory>()
            {
                new OrderHistory() {ClientId = 12345, Name = "Bogdan", LastName = "Datsenko", TotalPrice = 50},
                new OrderHistory() {ClientId = 12345, Name = "Bogdan", LastName = "Datsenko", TotalPrice = 110}
            };
        }
        private static IEnumerable<BasketItem> GetPreconfiguredBasketItems()
        {
            return new List<BasketItem>()
            {
                new BasketItem() {OrderHistoryId = 1, ItemId = 2, Name = ".Net", Price = 21, Amount = 1},
                new BasketItem() {OrderHistoryId = 1, ItemId = 6, Name = "Google", Price = 55, Amount = 2},
                new BasketItem() {OrderHistoryId = 2, ItemId = 3, Name = "Mozila", Price = 55, Amount = 1}
            };
        }
    }
}
