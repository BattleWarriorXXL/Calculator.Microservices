using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;
using Microsoft.Extensions.Logging;

namespace Calculator.Microservices.Shared.IntegrationEvents.EventHandling
{
    public class SubtractIntegrationEventHandler : IIntegrationEventHandler<SubtractIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<SubtractIntegrationEventHandler> _logger;

        public SubtractIntegrationEventHandler(IEventBus eventBus, ILogger<SubtractIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(SubtractIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(@event.A - @event.B);

            _logger.LogInformation($"Publish result: {@event.A} - {@event.B}");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
