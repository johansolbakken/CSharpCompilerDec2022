using System;
namespace gras.CodeAnalysis
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] m_tokens;
        private int m_position;
        private List<string> m_diagnostics = new();
        public IEnumerable<string> Diagnostics => m_diagnostics;
        private SyntaxToken m_current => Peek(0);

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);

            SyntaxToken token;
            do
            {
                token = lexer.Lex();
                if (token.Kind != SyntaxKind.WhitespaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EndOfFileToken);

            m_tokens = tokens.ToArray();

            m_diagnostics.AddRange(lexer.Diagnostics);
        }

        private SyntaxToken Peek(int offset)
        {
            var index = m_position + offset;
            if (index >= m_tokens.Length)
                return m_tokens[m_tokens.Length - 1];
            else
                return m_tokens[index];
        }

        private SyntaxToken NextToken()
        {
            var current = m_current;
            m_position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (m_current.Kind == kind)
                return NextToken();

            m_diagnostics.Add($"ERROR: Unexpected token <{m_current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, m_current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFileToken = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(Diagnostics, expression, endOfFileToken);
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            var left = ParsePrimaryExpression();

            while (true)
            {
                var precedence = SyntaxFacts.GetBinaryOperatorPresedence(m_current.Kind);
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                var operatorToken = NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (m_current.Kind == SyntaxKind.OpenParenToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.CloseParenToken);

                return new ParenthesizedExpressionSyntax(left, expression, right);
            }
            var numberToken = Match(SyntaxKind.LiteralToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
}

