Console.WriteLine();
//var mutex = new Mutex();

//using var messageHub = new MessageBus();

//Task.Run(() => messageHub.SubscribeOnTopic(Topics.RESULT_TOPIC, message => ShowResult(message), CancellationToken.None));

//ShowTip();

//while (true)
//{
//    Do();
//}

//void Do()
//{
//    var input = string.Empty;

//    switch (input = Console.ReadLine())
//    {
//        case "q":
//            return;
//        default:
//            ProcessCommand(input);
//            break;
//    }
//}

//static void ShowTip()
//{
//    Console.WriteLine("Support commands:");
//    Console.WriteLine("1. +");
//    Console.WriteLine("2. -");
//    Console.WriteLine("3. *");
//    Console.WriteLine("4. /");

//    Console.WriteLine("Using: 1.0 + 2.0");

//    Console.WriteLine("For exit use 'q'");
//}

//void ShowResult(Message result)
//{
//    Console.WriteLine($"= {result.Value}");
//}

//void ProcessCommand(string? consoleCommand)
//{
//    if (consoleCommand == null)
//    {
//        return;
//    }

//    var parameters = consoleCommand.Split(' ');
//    if (parameters.Length != 3)
//    {
//        return;
//    }

//    var command = ProcessParameters(parameters);
//    if (command == null)
//    {
//        return;
//    }

//    messageHub.SendMessage(Topics.ACTION_TOPIC, new Message(command));
//}

//static string? ProcessParameters(string[] parameters)
//{
//    double? a = double.TryParse(parameters[0], out var first) ? first : null;
//    if (a == null)
//    {
//        return null;
//    }

//    string? command = GetCommand(parameters[1]);
//    if (command == null)
//    {
//        return null;
//    }

//    double? b = double.TryParse(parameters[2], out var second) ? second : null;
//    if (b == null)
//    {
//        return null;
//    }

//    return $"{command} {a.Value} {b.Value}";
//}

//static string? GetCommand(string action)
//{
//    return action switch
//    {
//        "+" => Commands.ADD_COMMAND,
//        "-" => Commands.SUBTRACT_COMMAND,
//        "*" => Commands.MULTIPLY_COMMAND,
//        "/" => Commands.DIVIDE_COMMAND,
//        _ => null
//    };
//}