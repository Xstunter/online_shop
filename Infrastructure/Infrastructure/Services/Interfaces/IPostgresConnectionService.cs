using StackExchange.Redis;

namespace Infrastructure.Services.Interfaces
{
    public interface IRedisCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}