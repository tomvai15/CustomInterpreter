using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Expressions
{
    public class Unary : Expression
    {
        public Token Operator { get; private set; }
        public Expression Right { get; private set; }

        public Unary(Token _operator, Expression right)
        {
            Operator = _operator;
            Right = right;
        }

        public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
        {
            return expressionVisitor.VisitUnaryExpr(this);
        }
    }
}
