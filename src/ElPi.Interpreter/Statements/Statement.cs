using ElPi.Interpreter.Visitors;

namespace ElPi.Interpreter.Statements
{
    public abstract class Statement
    {
        public abstract T Accept<T>(IStatementVisitor<T> statementVisitor);
    }
}
