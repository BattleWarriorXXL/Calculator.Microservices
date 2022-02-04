using Confluent.Kafka;
using System.Text.Json;

namespace Calculator.Microservices.Shared.Kafka.Library
{
    public class Message : ISerializer<Message>, IDeserializer<Message>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Message()
        {
            Key = string.Empty;
            Value = string.Empty;
        }

        public Message (string value)
        {
            Key = string.Empty;
            Value = value;
        }

        public Message(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public byte[] Serialize(Message data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }

        public Message Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<Message>(data)!;
        }
    }
}
