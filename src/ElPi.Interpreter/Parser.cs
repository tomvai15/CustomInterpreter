using ElPi.Interpreter.Exceptions;
using ElPi.Interpreter.Expressions;
using ElPi.Interpreter.Statements;
using System;
using static ElPi.Interpreter.TokenType;

namespace ElPi.Interpreter
{
    public class Parser
    {
        public List<Token> Tokens { get; private set; }
        public int CurrentToken { get; private set; } = 0;

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public Expression Parse()
        {
            try
            {
                return CreateExpression();
            }
            catch (ParseException exception)
            {
                return null;
            }
        }

        public List<Statement> ParseStatements()
        {
            List<Statement> statements = new List<Statement>();

            while (!IsAtEnd())
            {
                statements.Add(CreateStatement());
            }

            return statements;
        }
        private Statement CreateStatement()
        {
            if (Match(PRINT)) 
            {
                return PrintStatement();
            }

            return ExpressionStatement();
        }

        private Statement PrintStatement()
        {
            Expression value = CreateExpression();
            Consume(SEMICOLON, "Expect ';' after value.");
            return new PrintStatement(value);
        }

        private Statement ExpressionStatement()
        {
            Expression expr = CreateExpression();
            Consume(SEMICOLON, "Expect ';' after expression.");
            return new ExpressionStatement(expr);
        }

        private Expression CreateExpression()
        {
            return GetEquality();
        }

        private Expression GetEquality()
        {
            Expression expr = GetComparison();
            while (Match(BANG_EQUAL, EQUAL_EQUAL))
            {
                Token @operator = Previous();
                Expression right = GetComparison();
                expr = new Binary(expr, @operator, right);
            }
            return expr;
        }

        private Expression GetComparison()
        {
            Expression expr = GetTerm();
            while (Match(GREATER, GREATER_EQUAL, LESS, LESS_EQUAL))
            {
                Token @operator = Previous();
                Expression right = GetTerm();
                expr = new Binary(expr, @operator, right);
            }
            return expr;
        }

        private Expression GetTerm()
        {
            Expression expr = GetFactor();
            while (Match(MINUS, PLUS))
            {
                Token @operator = Previous();
                Expression right = GetFactor();
                expr = new Binary(expr, @operator, right);
            }
            return expr;
        }

        private Expression GetFactor()
        {
            Expression expr = GetUnary();
            while (Match(SLASH, STAR))
            {
                Token @operator = Previous();
                Expression right = GetUnary();
                expr = new Binary(expr, @operator, right);
            }
            return expr;
        }

        private Expression GetUnary()
        {
            if (Match(BANG, MINUS))
            {
                Token @operator = Previous();
                Expression right = GetUnary();
                return new Unary(@operator, right);
            }
            return Primary();
        }

        private Expression Primary()
        {
            if (Match(FALSE)) return new Literal(false);
            if (Match(TRUE)) return new Literal(true);
            if (Match(NIL)) return new Literal(null);
            if (Match(NUMBER, STRING))
            {
                return new Literal(Previous().Literal);
            }
            if (Match(LEFT_PAREN))
            {
                Expression expr = CreateExpression();
                Consume(RIGHT_PAREN, "Expect ')' after expression.");
                return new Grouping(expr);
            }
            throw Error(Peek(), "Expect expression.");
        }

        private void Synchronize()
        {
            Advance();
            while (!IsAtEnd())
            {
                if (Previous().Type == SEMICOLON) return;
                switch (Peek().Type)
                {
                    case CLASS:
                    case FUN:
                    case VAR:
                    case FOR:
                    case IF:
                    case WHILE:
                    case PRINT:
                    case RETURN:
                        return;
                }
                Advance();
            }
        }

        private Token Consume(TokenType type, String message)
        {
            if (Check(type)) return Advance();
            throw Error(Peek(), message);
        }

        private ParseException Error(Token token, String message)
        {
            ErrorHandler.Error(token, message);
            return new ParseException();
        }

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) CurrentToken++;
            return Previous();
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        private bool IsAtEnd()
        {
            return Peek().Type == EOF;
        }
        private Token Peek()
        {
            return Tokens[CurrentToken];
        }
        private Token Previous()
        {
            return Tokens[CurrentToken - 1];
        }
    }
}
