namespace Order.Host.Models;

public class PaginatedOrderResponse<T>
{
    public IEnumerable<T> Data { get; init; } = null!;
}
