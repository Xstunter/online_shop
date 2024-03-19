#pragma warning disable CS8618

namespace Catalog.Host.Models.Response
{
    public class PaginatedItemByIdResponse<T> : PaginatedItemsResponse<T>
    {
        public int Id { get; set; }
    }
}
