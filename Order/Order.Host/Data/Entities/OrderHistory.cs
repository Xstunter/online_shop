﻿namespace Order.Host.Data.Entities
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; }

    }
}
