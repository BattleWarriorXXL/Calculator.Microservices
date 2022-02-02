var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

using var messageBus = new MessageBus(Environment.GetEnvironmentVariable("BOOTSTRAP_SERVERS") ?? "localhost:9092");
Task.Run(() => messageBus.SubscribeOnTopic<string>(Topics.ACTION_TOPIC, action =>
{
    var expressions = Commands.ParseCommand(action);
    if (expressions == null)
    {
        return;
    }

    var command = expressions.Value.Item1;
    if (command != Commands.MULTIPLY_COMMAND)
    {
        return;
    }

    var a = expressions.Value.Item2;
    var b = expressions.Value.Item3;

    messageBus.SendMessage(Topics.RESULT_TOPIC, (a * b).ToString());
}, CancellationToken.None));

app.Run();
