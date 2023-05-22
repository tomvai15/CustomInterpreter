namespace ElPi.App
{
    public static class ErrorHandler
    {
        public static void LogError(int line, string message)
        {
            Report(line, "", message);
        }
        private static void Report(int line, string where, string message)
        {
            Console.WriteLine($"[line {line}] Error {where} : {message}");
        }
    }
}
