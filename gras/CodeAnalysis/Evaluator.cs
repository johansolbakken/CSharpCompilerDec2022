using System;
namespace gras.CodeAnalysis
{
    public sealed class Evaluator
    {
        private readonly ExpressionSyntax root;

        public Evaluator(ExpressionSyntax root)
        {
            this.root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(root);
        }

        public int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax n)
            {
                return (int)n.NumberToken.Value;
            }

            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                    return left + right;
                else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                    return left - right;
                else if (b.OperatorToken.Kind == SyntaxKind.MultiplyToken)
                    return left * right;
                else if (b.OperatorToken.Kind == SyntaxKind.DivideToken)
                    return left / right;
                else
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
            }

            if (node is ParenthesizedExpressionSyntax p)
            {
                return EvaluateExpression(p.Expression);
            }

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}

