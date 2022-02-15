using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Pow.IntegrationEvents.EventHandling
{
    public class PowIntegrationEventHandler : IIntegrationEventHandler<PowIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<PowIntegrationEventHandler> _logger;

        public PowIntegrationEventHandler(IEventBus eventBus, ILogger<PowIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PowIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(Math.Pow(@event.X,@event.Y));

            _logger.LogInformation($"Publish result: {@event.X} ^ {@event.Y}");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
