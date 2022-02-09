using Calculator.Microservices.Shared.Kafka;
using Calculator.Microservices.Shared.Library;
using Calculator.Microservices.Shared.RabbitMQ;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Calculator.Microservices.Shared.Extensions
{
    public static class EventBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string target)
        {
            switch (Environment.GetEnvironmentVariable("BROKER_NAME"))
            {
                case "KAFKA":
                    var bootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS");
                    if (bootstrapServers == null)
                    {
                        throw new ArgumentNullException(nameof(bootstrapServers));
                    }
                    var groupId = Environment.GetEnvironmentVariable("KAFKA_GROUP_ID");
                    if (groupId == null)
                    {
                        throw new ArgumentNullException(nameof(groupId));
                    }

                    services.AddSingleton<IKafkaPersistentConnection>(sp =>
                    {
                        var logger = sp.GetRequiredService<ILogger<DefaultKafkaPersistentConnection>>();
                        var producerConfig = new ProducerConfig
                        {
                            BootstrapServers = bootstrapServers
                        };
                        var consumerConfig = new ConsumerConfig
                        {
                            BootstrapServers = bootstrapServers,
                            GroupId = groupId
                        };
                        var producerBuilder = new ProducerBuilder<string, byte[]>(producerConfig);
                        var consumerBuilder = new ConsumerBuilder<string, byte[]>(consumerConfig);

                        return new DefaultKafkaPersistentConnection(producerBuilder, consumerBuilder, logger, 5);
                    });

                    services.AddSingleton<IEventBus, EventBusKafka>(sp =>
                    {
                        var kafkaPersistentConnection = sp.GetRequiredService<IKafkaPersistentConnection>();
                        var logger = sp.GetRequiredService<ILogger<EventBusKafka>>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                        var retryCount = 5;
                        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EVENT_BUS_RETRY_COUNT")))
                        {
                            retryCount = int.Parse(Environment.GetEnvironmentVariable("EVENT_BUS_RETRY_COUNT")!);
                        }

                        return new EventBusKafka(kafkaPersistentConnection, logger, sp, eventBusSubcriptionsManager, target, retryCount);
                    });
                    break;
                case "RABBITMQ":
                    var hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME");
                    var rabbitmqQueue = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE");
                    if (rabbitmqQueue == null)
                    {
                        throw new ArgumentNullException(nameof(rabbitmqQueue));
                    }
                    if (hostName == null)
                    {
                        throw new ArgumentNullException(nameof(hostName));
                    }
                    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                    {
                        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                        var factory = new ConnectionFactory()
                        {
                            HostName = hostName,
                            DispatchConsumersAsync = true
                        };

                        var retryCount = 5;

                        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                    });

                    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                    {
                        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                        var retryCount = 5;
                        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EVENT_BUS_RETRY_COUNT")))
                        {
                            retryCount = int.Parse(Environment.GetEnvironmentVariable("EVENT_BUS_RETRY_COUNT")!);
                        }

                        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, target, retryCount);
                    });
                    break;
                default:
                    throw new ArgumentException("BROKER_NAME");
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IApplicationBuilder UseEventBus<T, TH>(this IApplicationBuilder app)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<T, TH>();

            return app;
        }
    }
}