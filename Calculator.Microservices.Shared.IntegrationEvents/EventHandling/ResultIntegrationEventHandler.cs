using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;
using Microsoft.Extensions.Logging;

namespace Calculator.Microservices.Shared.IntegrationEvents.EventHandling
{
    public class ResultIntegrationEventHandler : IIntegrationEventHandler<ResultIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ResultIntegrationEventHandler> _logger;

        public ResultIntegrationEventHandler(IEventBus eventBus, ILogger<ResultIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ResultIntegrationEvent @event)
        {
            _logger.LogInformation(@event.C.ToString());

            await Task.CompletedTask;
        }
    }
}
