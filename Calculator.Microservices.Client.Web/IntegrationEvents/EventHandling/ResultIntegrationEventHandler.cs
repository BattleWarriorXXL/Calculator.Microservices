using Calculator.Microservices.Client.Web.Services;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Client.Web.IntegrationEvents.EventHandling
{
    public class ResultIntegrationEventHandler : IIntegrationEventHandler<ResultIntegrationEvent>
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<ResultIntegrationEventHandler> _logger;

        public ResultIntegrationEventHandler(IMessageService messageService, ILogger<ResultIntegrationEventHandler> logger)
        {
            _messageService = messageService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ResultIntegrationEvent @event)
        {
            _logger.LogInformation(@event.C.ToString());
            
            _messageService.Notify(@event.C.ToString());

            await Task.CompletedTask;
        }
    }
}
