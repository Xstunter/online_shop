using StackExchange.Redis;

namespace Infrastructure.Services.Interfaces
{
    public interface IPostgresConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}