#pragma warning disable CS8618

namespace Catalog.Host.Models.Response
{
    public class PaginatedItemByBrandResponse<T> : PaginatedItemsResponse<T>
    {
        public string Brand { get; set; }
    }
}
