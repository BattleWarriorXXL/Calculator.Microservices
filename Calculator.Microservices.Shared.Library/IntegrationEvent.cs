using System.Text.Json.Serialization;

namespace Calculator.Microservices.Shared.Library
{
    public record IntegrationEvent
    {
        public IntegrationEvent(string target)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Target = target;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate, string target)
        {
            Id = id;
            CreationDate = createDate;
            Target = target;
        }

        [JsonInclude]
        public Guid Id { get; private init; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }

        [JsonInclude]
        public string Target { get; private init; }
    }
}
