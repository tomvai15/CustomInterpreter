using System;
using static ElPi.Interpreter.TokenType;

namespace ElPi.Interpreter
{
    public class Scanner
    {
        public string Source { get; private set; }
        public List<Token> Tokens { get; private set; } = new List<Token>();
        private int lexemeStart = 0;
        private int currentPosition = 0;
        private int line = 1;

        public Scanner(string source)
        {
            Source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                lexemeStart = currentPosition;
                ScanToken();
            }
            Tokens.Add(new Token(TokenType.EOF, "", null, line));
            return Tokens;
        }

        private bool IsAtEnd()
        {
            return currentPosition >= Source.Length;
        }

        private void ScanToken()
        {
            char character = Advance();
            switch (character)
            {
                case '(': AddToken(LEFT_PAREN); break;
                case ')': AddToken(RIGHT_PAREN); break;
                case '{': AddToken(LEFT_BRACE); break;
                case '}': AddToken(RIGHT_BRACE); break;
                case ',': AddToken(COMMA); break;
                case '.': AddToken(DOT); break;
                case '-': AddToken(MINUS); break;
                case '+': AddToken(PLUS); break;
                case ';': AddToken(SEMICOLON); break;
                case '*': AddToken(STAR); break;

                case '!': AddToken(Match('=') ? BANG_EQUAL : BANG); break;
                case '=': AddToken(Match('=') ? EQUAL_EQUAL : EQUAL); break;
                case '<': AddToken(Match('=') ? LESS_EQUAL : LESS); break;
                case '>': AddToken(Match('=') ? GREATER_EQUAL : GREATER); break;

                case '/':
                    if (Match('/'))
                    {
                        // A comment goes until the end of the line.
                        while (Peek() != '\n' && !IsAtEnd()) 
                        {
                            Advance();
                        };
                    }
                    else
                    {
                        AddToken(SLASH);
                    }
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    line++;
                    break;
                case '"': CreateString(); break;
                default:
                    if (char.IsDigit(character))
                    {
                        CreateNumber();
                    }
                    else if (IsAlpha(character))
                    {
                        CreateIdentifier();
                    }
                    else
                    {
                        throw new Exception($"{line} Unexpected character.");
                    }
                    break;
            }
        }

        private bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
            (c >= 'A' && c <= 'Z') ||
            c == '_';
        }

        private void CreateString()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n') line++;
                Advance();
            }
            if (IsAtEnd())
            {
                throw new Exception ($"{line} Unterminated string.");
            }
            // The closing ".
            Advance();
            string value = Source.Cut(lexemeStart + 1, currentPosition - 1);
            AddToken(STRING, value);
        }

        private void CreateNumber()
        {
            while (char.IsDigit(Peek())) 
            { 
                Advance(); 
            };
            // Look for a fractional part.
            if (Peek() == '.' && char.IsDigit(PeekNext()))
            {
                // Consume the "."
                Advance();
                while (char.IsDigit(Peek())) {
                    Advance();
                };
            }
            AddToken(NUMBER, double.Parse(Source.Cut(lexemeStart, currentPosition)));
        }

        private void CreateIdentifier()
        {
            while (isAlphaNumeric(Peek())) 
            {
                Advance();
            }

            string text = Source.Substring(lexemeStart, currentPosition);
            bool wasFound = Constants.Keywords.TryGetValue(text, out TokenType type);
            if (!wasFound) { 
                type = IDENTIFIER; 
            };

            AddToken(type); ;
        }

        private bool isAlphaNumeric(char c)
        {
            return IsAlpha(c) || char.IsDigit(c);
        }

        private char PeekNext()
        {
            if (currentPosition + 1 >= Source.Length) return '\0';
            return Source[currentPosition + 1];
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return Source[currentPosition];
        }

        private bool Match(char expected)
        {
            if (IsAtEnd()) return false;
            if (Source[currentPosition] != expected) return false;
            currentPosition++;
            return true;
        }

        private char Advance()
        {
            currentPosition++;
            return Source[currentPosition - 1];
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, object literal)
        {
            string text = Source.Cut(lexemeStart, currentPosition);
            Tokens.Add(new Token(type, text, literal, line));
        }
    }
}