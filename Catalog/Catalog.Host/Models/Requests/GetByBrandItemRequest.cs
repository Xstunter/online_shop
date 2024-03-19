#pragma warning disable CS8618

namespace Catalog.Host.Models.Requests
{
    public class GetByBrandItemRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Brand { get; set; }
    }
}
