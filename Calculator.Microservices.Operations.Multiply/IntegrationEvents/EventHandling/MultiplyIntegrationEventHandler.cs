using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Multiply.IntegrationEvents.EventHandling
{
    public class MultiplyIntegrationEventHandler : IIntegrationEventHandler<MultiplyIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<MultiplyIntegrationEventHandler> _logger;

        public MultiplyIntegrationEventHandler(IEventBus eventBus, ILogger<MultiplyIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(MultiplyIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(@event.A * @event.B);

            _logger.LogInformation($"Publish result: {@event.A} * {@event.B}");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
