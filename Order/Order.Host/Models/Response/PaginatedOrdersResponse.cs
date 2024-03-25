namespace Order.Host.Models.Response;

public class PaginatedOrdersResponse<T>
{
    public long Count { get; init; }

    public IEnumerable<T> Data { get; init; } = null!;
}
