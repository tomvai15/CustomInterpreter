using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Expressions
{
    public class Grouping : Expression
    {
        public Expression Expression { get; private set; }

        public Grouping(Expression expression)
        {
            Expression = expression;
        }

        public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
        {
            return expressionVisitor.VisitGroupingExpr(this);
        }
    }
}
