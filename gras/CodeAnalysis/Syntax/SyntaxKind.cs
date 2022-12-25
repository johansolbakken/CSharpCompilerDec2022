namespace gras.CodeAnalysis.Syntax

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
        LiteralExpression,
        BinaryExpression,
        ParenthesizedExpression,
        UnaryExpression
    }
}

