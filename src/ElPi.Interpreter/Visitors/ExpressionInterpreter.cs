using ElPi.Interpreter.Exceptions;
using ElPi.Interpreter.Expressions;
using static ElPi.Interpreter.TokenType;

namespace ElPi.Interpreter.Visitors
{
    public class ExpressionInterpreter : IExpressionVisitor<object>
    {
        public object Interpret(Expression expression)
        {
            return Evaluate(expression);
        }

        public object VisitBinaryExpr(Binary expr)
        {
            object left = Evaluate(expr.Left);
            object right = Evaluate(expr.Right);
            switch (expr.Operator.Type) 
            {
                case BANG_EQUAL: 
                    return !IsEqual(left, right);
                case EQUAL_EQUAL: 
                    return IsEqual(left, right);
                case GREATER:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left > (double)right;
                case GREATER_EQUAL:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left >= (double)right;
                case LESS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left < (double)right;
                case LESS_EQUAL:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left <= (double)right;
                case MINUS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left - (double)right;
                case PLUS:
                    if (left is double && right is double) {
                        return (double)left + (double)right;
                    }
                    if (left is string && right is string) {
                        return (string)left + (string)right;
                    }
                    throw new RuntimeException(expr.Operator, "Operands must be two numbers or two strings.");
                    break;
                case SLASH:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left / (double)right;
                case STAR:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left * (double)right;
            }
            // Unreachable.
            return null;
        }

        private bool IsEqual(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;
            return a.Equals(b);
        }

        public object VisitGroupingExpr(Grouping expr)
        {
            return Evaluate(expr);
        }

        public object VisitLiteralExpr(Literal expr)
        {
            return expr.Value;
        }

        public object VisitUnaryExpr(Unary expr)
        {
            object right = Evaluate(expr.Right);
            switch (expr.Operator.Type) {
                case BANG:
                    return !IsTruthy(right);
                case MINUS:
                    return -(double)right;
            }
            // Unreachable.
            return null;
        }

        private void CheckNumberOperands(Token @operator, object left, object right)
        {
            if (left is double && right is double) return;

            throw new RuntimeException(@operator, "Operands must be numbers.");
        }

        private bool IsTruthy(Object obj)
        {
            if (obj == null) return false;
            if (obj is bool) return (bool)obj;
            return true;
        }

        private object Evaluate(Expression expr)
        {
            return expr.Accept(this);
        }
    }
}
