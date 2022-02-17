using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace Calculator.Microservices.Shared.Library.HealthCheck.RabbitMQHealthCheck
{
    public class RabbitMQHealthCheck : IHealthCheck
    {
        private IConnection? _connection;
        private IConnectionFactory? _factory;

        private readonly string _rabbitMQHostName;
        private readonly SslOption? _sslOption;

        public RabbitMQHealthCheck(string rabbitMQHostName, SslOption? sslOption)
        {
            _rabbitMQHostName = rabbitMQHostName;
            _sslOption = sslOption;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                EnsureConnection();

                using (_connection?.CreateModel())
                {
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, exception: ex));
            }
        }

        private void EnsureConnection()
        {
            if (_connection == null)
            {
                if (_factory == null)
                {
                    _factory = new ConnectionFactory()
                    {
                        HostName = _rabbitMQHostName,
                        AutomaticRecoveryEnabled = true,
                        UseBackgroundThreadsForIO = true
                    };

                    if (_sslOption != null)
                    {
                        ((ConnectionFactory)_factory).Ssl = _sslOption;
                    }
                }

                _connection = _factory.CreateConnection();
            }
        }
    }
}
