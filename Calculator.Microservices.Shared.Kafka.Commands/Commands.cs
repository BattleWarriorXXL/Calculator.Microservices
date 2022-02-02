namespace Calculator.Microservices.Shared.Kafka.Commands
{
    public static class Commands
    {
        public const string ADD_COMMAND = "add_command";
        public const string SUBTRACT_COMMAND = "subtract_command";
        public const string MULTIPLY_COMMAND = "multiply_command";
        public const string DIVIDE_COMMAND = "divide_command";

        public static string Add(double a, double b)
        {
            return PerformCommand(ADD_COMMAND, a, b);
        }

        public static string Subtract(double a, double b)
        {
            return PerformCommand(SUBTRACT_COMMAND, a, b);
        }

        public static string Multiply(double a, double b)
        {
            return PerformCommand(MULTIPLY_COMMAND, a, b);
        }

        public static string Divide(double a, double b)
        {
            return PerformCommand(DIVIDE_COMMAND, a, b);
        }

        public static (string, double, double)? ParseCommand(string commandExpression)
        {
            var parameters = commandExpression.Split(' ');
            if (parameters.Length != 3)
            {
                return null;
            }

            var command = parameters[0];
            if (!CheckCommand(command))
            {
                return null;
            }

            double? a = double.TryParse(parameters[1], out var first) ? first : null;
            if (a == null)
            {
                return null;
            }

            double? b = double.TryParse(parameters[2], out var second) ? second : null;
            if (b == null)
            {
                return null;
            }

            return (command, a.Value, b.Value);

        }

        private static string PerformCommand(string command, double a, double b)
        {
            return $"{command} {a} {b}";
        }

        private static bool CheckCommand(string command)
        {
            return command switch
            {
                ADD_COMMAND => true,
                SUBTRACT_COMMAND => true,
                MULTIPLY_COMMAND => true,
                DIVIDE_COMMAND => true,
                _ => false
            };
        }
    }
}