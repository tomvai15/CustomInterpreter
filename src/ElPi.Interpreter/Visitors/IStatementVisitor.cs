using ElPi.Interpreter.Statements;

namespace ElPi.Interpreter.Visitors
{
    public interface IStatementVisitor<T>
    {
        T VisitPrintStatement(PrintStatement printStatement);
        T VisitExpressionStatement(ExpressionStatement expressionStatement);
    }
}
