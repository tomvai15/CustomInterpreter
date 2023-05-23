using ElPi.Interpreter.Expressions;
using System.Text;

namespace ElPi.Interpreter.Visitors
{
    public class AstPrinter : IExpressionVisitor<string>
    {
        public string Print(Expression expr)
        {
            return expr.Accept(this);
        }

        public string VisitBinaryExpr(Binary binaryExpression)
        {
            return Parenthesize(binaryExpression.Operator.Lexeme, binaryExpression.Left, binaryExpression.Right);
        }

        public string VisitGroupingExpr(Grouping groupingExpression)
        {
            return Parenthesize("group", groupingExpression.Expression);
        }

        public string VisitLiteralExpr(Literal literalExpression)
        {
            if (literalExpression.Value == null) return "nil";
            return literalExpression.Value.ToString();
        }

        public string VisitUnaryExpr(Unary unaryExpression)
        {
            return Parenthesize(unaryExpression.Operator.Lexeme, unaryExpression.Right);
        }

        private string Parenthesize(string name, params Expression[] expressions)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("(").Append(name);
            foreach (Expression expr in expressions)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");
            return builder.ToString();
        }
    }
}
