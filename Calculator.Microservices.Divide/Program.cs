var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

using var messageBus = new MessageBus();
Task.Run(() => messageBus.SubscribeOnTopic(Topics.ACTION_TOPIC, message =>
{
    var expressions = Commands.ParseCommand(message.Value);
    if (expressions == null)
    {
        return;
    }

    var command = expressions.Value.Item1;
    if (command != Commands.DIVIDE_COMMAND)
    {
        return;
    }

    var a = expressions.Value.Item2;
    var b = expressions.Value.Item3;

    messageBus.SendMessage(Topics.RESULT_TOPIC, new Message(message.Key, (a / b).ToString()));
}, CancellationToken.None));

app.Run();
