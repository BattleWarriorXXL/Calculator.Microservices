using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Sqrt.IntegrationEvents.EventHandling
{
    public class SqrtIntegrationEventHandler : IIntegrationEventHandler<SqrtIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<SqrtIntegrationEventHandler> _logger;

        public SqrtIntegrationEventHandler(IEventBus eventBus, ILogger<SqrtIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(SqrtIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(Math.Sqrt(@event.D));

            _logger.LogInformation($"Publish result: Sqrt({@event.D})");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
