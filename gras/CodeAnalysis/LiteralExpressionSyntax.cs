namespace gras.CodeAnalysis
{
    internal sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public SyntaxToken NumberToken { get; }

        public LiteralExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}
