#pragma warning disable CS8618

namespace Catalog.Host.Models.Requests
{
    public class GetByIdItemRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public int Id { get; set; }
    }
}
