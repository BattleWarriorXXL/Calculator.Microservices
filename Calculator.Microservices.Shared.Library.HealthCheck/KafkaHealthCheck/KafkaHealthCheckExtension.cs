using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Calculator.Microservices.Shared.Library.HealthCheck.KafkaHealthCheck
{
    public static class KafkaHealthCheckExtension
    {
        private const string NAME = "kafka";

        public static IHealthChecksBuilder AddKafkaCheck(this IHealthChecksBuilder builder, string kafkaBootstrapServers,
                                                            string? name = default, HealthStatus? failureStatus = default,
                                                            IEnumerable<string>? tags = default, TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name ?? NAME,
                new KafkaHealthCheck(kafkaBootstrapServers),
                failureStatus,
                tags,
                timeout
                ));
        }
    }
}
