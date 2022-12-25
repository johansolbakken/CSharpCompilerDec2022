using System;

namespace gras.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {
        private readonly string m_text;
        private int m_position;
        private readonly List<string> m_diagnostics = new();
        public IEnumerable<string> Diagnostics => m_diagnostics;

        public Lexer(string text)
        {
            m_text = text;
        }

        private char Current
        {
            get
            {
                if (m_position >= m_text.Length)
                {
                    return '\0';
                }
                return m_text[m_position];
            }
        }

        private void next()
        {
            m_position++;
        }

        // A very nice data structure!
        private readonly Dictionary<char, SyntaxKind> tokenDict = new()
        {
            {'+', SyntaxKind.PlusToken },
            {'-', SyntaxKind.MinusToken },
            {'*', SyntaxKind.MultiplyToken },
            {'/', SyntaxKind.DivideToken },
            {'(', SyntaxKind.OpenParenToken },
            {')', SyntaxKind.CloseParenToken }
        };

        // A lexer lexes haha
        public SyntaxToken Lex()
        {
            // End of file
            if (m_position >= m_text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, m_position, "\0", null);
            }

            // Numbers
            if (char.IsDigit(Current))
            {
                var start = m_position;

                while (char.IsDigit(Current))
                    next();

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                    m_diagnostics.Add($"ERROR: Failed to parse int32 '{text}'");
                return new SyntaxToken(SyntaxKind.LiteralToken, start, text, value);
            }

            // Whitespace
            if (char.IsWhiteSpace(Current))
            {
                var start = m_position;
                while (char.IsWhiteSpace(Current))
                    next();

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, text);
            }

            // Operators
            if (tokenDict.ContainsKey(Current))
            {
                var token = new SyntaxToken(tokenDict[Current], m_position, Current.ToString(), null);
                next();
                return token;
            }

            m_diagnostics.Add($"ERROR: Bad character input: '{Current}'");
            var token1 = new SyntaxToken(SyntaxKind.BadToken, m_position, Current.ToString(), null);
            next();
            return token1;
        }
    }
}

