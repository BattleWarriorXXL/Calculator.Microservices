namespace Calculator.Microservices.Shared.Kafka.Commands
{
    public static class Commands
    {
        private const string ADD_COMMAND = "add_command";
        private const string SUBTRACT_COMMAND = "subtract_command";
        private const string MULTIPLY_COMMAND = "multiply_command";
        private const string DIVIDE_COMMAND = "divide_command";

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

        private static string PerformCommand(string command, double a, double b)
        {
            return $"{command} {a} {b}";
        }
    }
}