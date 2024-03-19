using Infrastructure.Configuration;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class PostgresConnectionService : IPostgresConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public PostgresConnectionService(
            IOptions<PostgresConfig> config)
        {
            var postgresConfigurationOptions = ConfigurationOptions.Parse(config.Value.Host);
            _connectionLazy =
                new Lazy<ConnectionMultiplexer>(() 
                    => ConnectionMultiplexer.Connect(postgresConfigurationOptions));
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