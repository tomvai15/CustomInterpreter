using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Expressions
{
    public class Literal : Expression
    {
        public object Value { get; private set; }

        public Literal(object value)
        {
            Value = value;
        }

        public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
        {
            return expressionVisitor.VisitLiteralExpr(this);
        }
    }
}
