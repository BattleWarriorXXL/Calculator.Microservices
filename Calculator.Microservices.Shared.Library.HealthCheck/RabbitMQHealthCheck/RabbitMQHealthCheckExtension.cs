using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace Calculator.Microservices.Shared.Library.HealthCheck.RabbitMQHealthCheck
{
    public static class RabbitMQHealthCheckExtension
    {
        private const string NAME = "rabbitmq";

        public static IHealthChecksBuilder AddRabbitMQCheck(this IHealthChecksBuilder builder, string rabbitMQHostName,
                                                            SslOption? sslOption = default, string? name = default,
                                                            HealthStatus? failureStatus = default, IEnumerable<string>? tags = default,
                                                            TimeSpan? timeout = default)
        {
            builder.Services.AddSingleton(sp => new RabbitMQHealthCheck(rabbitMQHostName, sslOption));

            return builder.Add(new HealthCheckRegistration(
                name ?? NAME,
                sp => sp.GetRequiredService<RabbitMQHealthCheck>(),
                failureStatus,
                tags,
                timeout
                ));
        }
    }
}
