#pragma warning disable CS8618

namespace Catalog.Host.Models.Requests
{
    public class GetByTypeItemRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public string Type { get; set; }
    }
}
