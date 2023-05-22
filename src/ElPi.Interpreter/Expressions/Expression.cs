using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Expressions
{
    public abstract class Expression
    {
        public abstract T Accept<T>(IExpressionVisitor<T> expressionVisitor);
    }
}
