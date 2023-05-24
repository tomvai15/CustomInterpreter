namespace ElPi.Interpreter.Exceptions
{
    public class RuntimeException: Exception
    {
        public Token Token { get; set; }

        public RuntimeException(Token token, string message): base(message)
        {
            Token = token;
        }
    }
}
