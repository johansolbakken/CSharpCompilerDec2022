using System;

namespace gras.CodeAnalysis
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public int Position { get; }
        public string Text { get; }
        public override SyntaxKind Kind { get; }
        public object Value { get; }

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Position = position;
            Kind = kind;
            Text = text;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}

