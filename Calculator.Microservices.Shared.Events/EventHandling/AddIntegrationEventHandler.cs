using Calculator.Microservices.Operations.Add.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.Events.Events
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
            var 
            _eventBus.Publish()

            await Task.CompletedTask;
        }
    }
}
