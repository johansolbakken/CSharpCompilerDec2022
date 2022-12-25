namespace gras.CodeAnalysis.Syntax
{
    internal sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken LiteralToken { get; }

        public LiteralExpressionSyntax(SyntaxToken numberToken)
        {
            LiteralToken = numberToken;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}
