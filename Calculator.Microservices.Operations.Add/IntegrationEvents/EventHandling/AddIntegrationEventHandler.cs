using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Add.IntegrationEvents.EventHandling
{
    public class AddIntegrationEventHandler : IIntegrationEventHandler<AddIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<AddIntegrationEventHandler> _logger;

        public AddIntegrationEventHandler(IEventBus eventBus, ILogger<AddIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AddIntegrationEvent @event)
        {
            var resultEvent = new ResultIntegrationEvent(@event.A + @event.B);

            _logger.LogInformation($"Publish result: {@event.A} + {@event.B}");

            _eventBus.Publish(resultEvent);

            await Task.CompletedTask;
        }
    }
}
