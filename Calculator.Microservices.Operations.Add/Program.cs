using Calculator.Microservices.Shared.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Kafka;
using Calculator.Microservices.Shared.Library;
using Calculator.Microservices.Shared.RabbitMQ;
using Confluent.Kafka;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

if ("kafka" == "kafka")
{
    builder.Services.AddSingleton<IKafkaPersistentConnection>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<DefaultKafkaPersistentConnection>>();
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:19092"
        };
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:19092",
            GroupId = "custom-group"
        };
        var producerBuilder = new ProducerBuilder<string, byte[]>(producerConfig);
        var consumerBuilder = new ConsumerBuilder<string, byte[]>(consumerConfig);

        return new DefaultKafkaPersistentConnection(producerBuilder, consumerBuilder, logger, 5);
    });

    builder.Services.AddSingleton<IEventBus, EventBusKafka>(sp =>
    {
        var subscriptionClientName = "topic";
        var kafkaPersistentConnection = sp.GetRequiredService<IKafkaPersistentConnection>();
        var logger = sp.GetRequiredService<ILogger<EventBusKafka>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var retryCount = 5;

        return new EventBusKafka(kafkaPersistentConnection, logger, sp, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    });
}
else if ("rabbitmq" == "rabbitmq")
{
    builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };

        var retryCount = 5;

        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    });

    builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    {
        string? subscriptionClientName = null;
        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var retryCount = 5;

        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    });
}

builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

builder.Services.AddTransient<AddIntegrationEventHandler>();

var app = builder.Build();
ConfigureEventBus(app);

app.Run();

void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<AddIntegrationEvent, AddIntegrationEventHandler>();
}