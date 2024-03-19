#pragma warning disable CS8618

namespace Catalog.Host.Models.Response
{
    public class PaginatedItemByTypeResponse<T> : PaginatedItemsResponse<T>
    {
        public string Type { get; set; }
    }
}
