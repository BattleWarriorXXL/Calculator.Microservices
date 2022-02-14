using Calculator.Microservices.Shared.Kafka;
using Calculator.Microservices.Shared.Library;
using Calculator.Microservices.Shared.RabbitMQ;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Calculator.Microservices.Shared.Extensions
{
    public static class EventBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration["BROKER_TYPE"])
            {
                case "KAFKA":
                    var bootstrapServers = configuration["KAFKA_BOOTSTRAP_SERVERS"];
                    if (bootstrapServers == null)
                    {
                        throw new ArgumentNullException(nameof(bootstrapServers));
                    }

                    var groupId = configuration["KAFKA_GROUP_ID"] ?? "custom-group";

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
                        if (!string.IsNullOrEmpty(configuration["EVENT_BUS_RETRY_COUNT"]))
                        {
                            retryCount = int.Parse(configuration["EVENT_BUS_RETRY_COUNT"]);
                        }

                        return new EventBusKafka(kafkaPersistentConnection, logger, sp, eventBusSubcriptionsManager, configuration["TARGET"], retryCount);
                    });
                    break;
                case "RABBITMQ":
                    var hostName = configuration["RABBITMQ_HOSTNAME"];
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
                        if (!string.IsNullOrEmpty(configuration["EVENT_BUS_RETRY_COUNT"]))
                        {
                            retryCount = int.Parse(configuration["EVENT_BUS_RETRY_COUNT"]!);
                        }

                        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, configuration["TARGET"], retryCount);
                    });
                    break;
                default:
                    throw new ArgumentException("BROKER_TYPE");
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IApplicationBuilder Subscribe<T, TH>(this IApplicationBuilder app)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<T, TH>();

            return app;
        }

        public static IApplicationBuilder SubscribeWithCallback<T, TH, TC>(this IApplicationBuilder app)
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<T, TH>();

            return app;
        }
    }
}