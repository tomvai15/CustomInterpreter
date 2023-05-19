using static ElPi.Interpreter.TokenType;

namespace ElPi.Interpreter
{
    public static class Constants
    {
        public static Dictionary<string, TokenType> Keywords = new() {
            {"and", AND},
            {"class", CLASS},
            {"else", ELSE},
            {"false", FALSE},
            {"for", FOR},
            {"fun", FUN},
            {"if", IF},
            {"nil", NIL},
            {"or", OR},
            {"print", PRINT},
            {"return", RETURN},
            {"super", SUPER},
            {"this", THIS},
            {"true", TRUE},
            {"var", VAR},
            {"while", WHILE},
        };
    }
}
