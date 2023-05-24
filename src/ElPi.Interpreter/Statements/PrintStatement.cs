using ElPi.Interpreter.Expressions;
using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Statements
{
    public class PrintStatement: Statement
    {
        public Expression Expression { get; set; }

        public PrintStatement(Expression expression)
        {
            Expression = expression;
        }

        public override T Accept<T>(IStatementVisitor<T> statementVisitor)
        {
            return statementVisitor.VisitPrintStatement(this);
        }
    }
}
