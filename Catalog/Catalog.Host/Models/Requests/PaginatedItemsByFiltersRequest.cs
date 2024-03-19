namespace Catalog.Host.Models.Requests
{
    public class PaginatedItemsByFiltersRequest<T>
    where T : notnull
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Dictionary<T, int>? Filters { get; set; }
    }
}
