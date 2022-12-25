namespace gras.CodeAnalysis.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public override Type Type => Left.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
        public BoundBinaryOperatorKind OperatorKind { get; }
        public BoundExpression Left { get; }
        public BoundExpression Right { get; }

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right)
        {
            OperatorKind = operatorKind;
            Right = right;
            Left = left;
        }
    }
}

