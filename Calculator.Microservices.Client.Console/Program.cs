Console.WriteLine("Starting console client of calculator...");
var consoleThread = Thread.CurrentThread;

using var messageHub = new MessageBus();

Task.Run(() => messageHub.SubscribeOnTopic<string>(Topics.RESULT_TOPIC, message => ShowResult(message), CancellationToken.None));

var input = string.Empty;

do
{
    ShowTip();

    switch (input = Console.ReadLine())
    {
        case "q":
            return;
        default:
            ProcessCommand(input);
            consoleThread.Suspend();
            break;
    }
    
} while (true);

static void ShowTip()
{
    Console.WriteLine("Support commands:");
    Console.WriteLine("1. +");
    Console.WriteLine("2. -");
    Console.WriteLine("3. *");
    Console.WriteLine("4. /");

    Console.WriteLine("Using: 1.0 + 2.0");

    Console.WriteLine("For exit use 'q'");
}

void ShowResult(string result)
{
    consoleThread.Resume();
    Console.WriteLine($"Result {result}");
}

void ProcessCommand(string? consoleCommand)
{
    if (consoleCommand == null)
    {
        return;
    }

    var parameters = consoleCommand.Split(' ');
    if (parameters.Length != 3)
    {
        return;
    }

    var command = ProcessParameters(parameters);
    if (command == null)
    {
        return;
    }

    messageHub.SendMessage(Topics.ACTION_TOPIC, command);
}

static string? ProcessParameters(string[] parameters)
{
    double? a = double.TryParse(parameters[0], out var first) ? first : null;
    if (a == null)
    {
        return null;
    }

    string? command = GetCommand(parameters[1]);
    if (command == null)
    {
        return null;
    }

    double? b = double.TryParse(parameters[2], out var second) ? second : null;
    if (b == null)
    {
        return null;
    }

    return $"{command} {a.Value} {b.Value}";
}

static string? GetCommand(string action)
{
    return action switch
    {
        "+" => Commands.ADD_COMMAND,
        "-" => Commands.SUBTRACT_COMMAND,
        "*" => Commands.MULTIPLY_COMMAND,
        "/" => Commands.DIVIDE_COMMAND,
        _ => null
    };
}