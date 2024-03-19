using Infrastructure.Configuration;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public RedisCacheConnectionService(
            IOptions<RedisConfig> config)
        {
            var redisConfigurationOptions = ConfigurationOptions.Parse(config.Value.Host);
            _connectionLazy =
                new Lazy<ConnectionMultiplexer>(() 
                    => ConnectionMultiplexer.Connect(redisConfigurationOptions));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}