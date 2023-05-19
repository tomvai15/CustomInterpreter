using ElPi.Interpreter;
using ElPi.Interpreter.Expressions;
using ElPi.Interpreter.Visitors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPi.UnitTests.Interpreter.Visitor
{
    public class AstPrinterTests
    {
        private readonly AstPrinter _printer = new AstPrinter();

        [Fact]
        public void VisitUnaryExpr_ReturnsOperatorAndExpression()
        {
            // Arrange
            var _operator = "+";
            var value = Any<int>();

            var token = new Token(TokenType.PLUS, _operator, null, 0);
            var expression = new Literal(value);
            var unary = new Unary(token, expression);

            // Act
            var result = unary.Accept(_printer);

            // Assert
            result.Should().Be($"({_operator} {value.ToString()})");
        }

        [Fact]
        public void VisitLiteralExpr_ReturnsLiteral()
        {
            // Arrange
            var value = Any<int>();
            var literal = new Literal(value);

            // Act
            var result = literal.Accept(_printer);

            // Assert
            result.Should().Be(value.ToString());
        }

        [Fact]
        public void VisitGroupingExpr_ReturnsExpression()
        {
            // Arrange
            var value = Any<int>();

            var expression = new Literal(value);
            var unary = new Grouping(expression);

            // Act
            var result = unary.Accept(_printer);

            // Assert
            result.Should().Be($"(group {value.ToString()})");
        }

        [Fact]
        public void VisitBinaryExpr_ReturnsLeftExpression_Operator_RightExpression()
        {
            // Arrange
            var _operator = "+";
            var left = Any<int>();
            var right = Any<int>();

            var token = new Token(TokenType.PLUS, _operator, null, 0);
            var leftExpression = new Literal(left);
            var rightExpression = new Literal(right);
            var unary = new Binary(leftExpression, token, rightExpression);

            // Act
            var result = unary.Accept(_printer);

            // Assert
            result.Should().Be($"({_operator} {left.ToString()} {right.ToString()})");
        }
    }
}
