namespace Order.Host.Data;

public class PaginatedOrder<T>
{
    public long TotalCount { get; init; }

    public IEnumerable<T> Data { get; init; } = null!;
}