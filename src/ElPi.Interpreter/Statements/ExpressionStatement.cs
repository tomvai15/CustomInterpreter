using ElPi.Interpreter.Expressions;
using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Statements
{
    public class ExpressionStatement: Statement
    {
        public Expression Expression { get; set; }

        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
        }

        public override T Accept<T>(IStatementVisitor<T> statementVisitor)
        {
            return statementVisitor.VisitExpressionStatement(this);
        }
    }
}
