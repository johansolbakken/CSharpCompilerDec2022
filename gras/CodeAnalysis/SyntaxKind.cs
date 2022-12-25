namespace gras.CodeAnalysis

{
    public enum SyntaxKind
    {
        // Tokens
        EndOfFileToken,
        BadToken,
        WhitespaceToken,

        LiteralToken,

        PlusToken,
        MinusToken,
        MultiplyToken,
        DivideToken,

        OpenParenToken,
        CloseParenToken,

        // Expressions
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}

