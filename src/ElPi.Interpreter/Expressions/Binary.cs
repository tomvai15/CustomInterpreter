using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Expressions
{
    public class Binary : Expression
    {
        public Expression Left { get; private set; }
        public Token Operator { get; private set; }
        public Expression Right { get; private set; }

        public Binary(Expression left, Token _operator, Expression right)
        {
            Left = left;
            Operator = _operator;
            Right = right;
        }

        public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
        {
            return expressionVisitor.VisitBinaryExpr(this);
        }
    }
}
