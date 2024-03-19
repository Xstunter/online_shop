namespace Catalog.Host.Models.Requests
{
    public class PaginatedBrandsRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
