namespace Basket.Host.Models.Request
{
    public class GetByIdItemRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Id { get; set; }
    }
}
