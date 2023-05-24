using ElPi.Interpreter.Expressions;

namespace ElPi.Interpreter.Visitors
{
    public interface IExpressionVisitor<T>
    {
        T VisitBinaryExpr(Binary expr);
        T VisitGroupingExpr(Grouping expr);
        T VisitLiteralExpr(Literal expr);
        T VisitUnaryExpr(Unary expr);
    }
}
