namespace ElPi.Interpreter.Exceptions
{
    public class ParseException: Exception
    {
        public ParseException() { }

        public ParseException(string message): base(message) { }
    }
}
