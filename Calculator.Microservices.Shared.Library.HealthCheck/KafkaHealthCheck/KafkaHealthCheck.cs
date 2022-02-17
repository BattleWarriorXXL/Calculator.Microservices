using Confluent.Kafka;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Calculator.Microservices.Shared.Library.HealthCheck.KafkaHealthCheck
{
    public class KafkaHealthCheck : IHealthCheck
    {
        private readonly string _kafkaBootstrapServers;
        private readonly ProducerConfig _producerConfig;

        private IProducer<string, string>? _producer;

        public KafkaHealthCheck(string kafkaBootstrapServers)
        {
            _kafkaBootstrapServers = kafkaBootstrapServers;
            _producerConfig = new ProducerConfig()
            {
                BootstrapServers = _kafkaBootstrapServers
            };
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_producer == null)
                {
                    _producer = new ProducerBuilder<string, string>(_producerConfig).Build();
                }

                var message = new Message<string, string>()
                {
                    Key = "healthcheck-key",
                    Value = $"Check Kafka health on {DateTime.UtcNow}"
                };

                var result = await _producer.ProduceAsync("healthchecks-topic", message, cancellationToken);

                if (result.Status == PersistenceStatus.NotPersisted)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, description: $"Message is not persisted or a failure is raised on health check for kafka.");
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
