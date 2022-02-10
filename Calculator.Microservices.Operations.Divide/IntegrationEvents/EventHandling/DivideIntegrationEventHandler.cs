using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Divide.IntegrationEvents.EventHandling
{
    public class DivideIntegrationEventHandler : IIntegrationEventHandler<DivideIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<DivideIntegrationEventHandler> _logger;

        public DivideIntegrationEventHandler(IEventBus eventBus, ILogger<DivideIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DivideIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(@event.A / @event.B);

            _logger.LogInformation($"Publish result: {@event.A} / {@event.B}");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
