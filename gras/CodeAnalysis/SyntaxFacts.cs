namespace gras.CodeAnalysis
{
    internal static class SyntaxFacts
    {
        public static int GetBinaryOperatorPresedence(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;
                case SyntaxKind.MultiplyToken:
                case SyntaxKind.DivideToken:
                    return 2;
                case SyntaxKind.OpenParenToken:
                case SyntaxKind.CloseParenToken:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}

