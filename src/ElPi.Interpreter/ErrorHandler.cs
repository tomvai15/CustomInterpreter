namespace ElPi.Interpreter
{
    public static class ErrorHandler
    {
        public static void LogError(int line, string message)
        {
            Report(line, "", message);
        }
        public static void Report(int line, string where, string message)
        {
            Console.WriteLine($"[line {line}] Error {where} : {message}");
        }

        public static void Error(Token token, String message)
        {
            if (token.Type == TokenType.EOF)
            {
                Report(token.Line, " at end", message);
            }
            else
            {
                Report(token.Line, " at '" + token.Lexeme + "'", message);
            }
        }
    }
}
